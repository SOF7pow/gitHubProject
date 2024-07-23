using System.Collections;
using _gitProject.logic.Components;
using _gitProject.logic.Components.Labels;
using _gitProject.logic.Events;
using _gitProject.logic.ExtensionMethods;
using _gitProject.logic.Interfaces;
using _gitProject.logic.ObjectsPool;
using _gitProject.logic.Services;
using _gitProject.logic.Storage;
using _gitProject.logic.ViewCamera;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace _gitProject.logic.Player {
    
    [RequireComponent(typeof(CharacterController),typeof(AudioSource))]
    public class PlayerController : MonoBehaviour, IService, IHealeable {

        #region fields
        
        [SerializeField, UnityEngine.Min(1)] private int maxHealth = 100;
        [SerializeField, UnityEngine.Min(1), Max(20)] private int startMoveSpeed = 10;
        [SerializeField, UnityEngine.Min(1), Max(20)] private int startDamage = 1;
        [SerializeField,UnityEngine.Min(0)] private float jumpForce;
        [SerializeField,UnityEngine.Min(0)] private float rotateSpeed;
        [SerializeField,UnityEngine.Min(0)] private float fireRate;
        [SerializeField,UnityEngine.Min(1)] private int damageMultiplyerKoef;
        
        private GameObjectPool _popUpPool;
        private Shoot _shoot;
        private Health _health;
        private Movement _movement;
        private MouseLook _mouseLook;
        private InputHandler _inputHandler;
        private ColorChanger _colorChanger;
        private SoundReaction _soundReaction;
        private VisualReaction _visualReaction;
        private LaserLabel _laser;
        private Transform _laserContainer;
        private Transform _muzzle;
        private float _moveSpeed;
        private int _damage;
        private bool _isActive;
        
        #endregion

        #region initialization

        public void Init() {
            _popUpPool = new GameObjectPool(ServiceLocator.Current.Get<PrefabsStorage>().Storage.PopUpDamage, 
                "PlayerContainer", new GameObject().transform,5);
            
            _muzzle ??= GetComponentInChildren<MuzzleLabel>().transform;
            _laserContainer ??= _muzzle.GetComponentInChildren<LaserContainerLabel>().transform;
            _moveSpeed = startMoveSpeed;
            _damage = startDamage;
            
            _health = new Health(maxHealth);
            _movement = new (GetComponent<CharacterController>());
            _colorChanger = new ColorChanger(_health.GetHealth, GetComponent<Renderer>(), Color.white, Color.red);
            _mouseLook = new (transform, rotateSpeed);
            _inputHandler = new (transform, ServiceLocator.Current.Get<CameraBehaviour>().GetComponentInChildren<Camera>());
            _shoot = new (_muzzle, fireRate, damageMultiplyerKoef);
            _soundReaction = new(GetComponent<AudioSource>());
            _visualReaction = new VisualReaction(this, transform);
            
            Enable();
        }

        

        #endregion
        
        #region unity callbacks
        private void OnDisable() {
            Dispose();
        }

       

        private void Update() {
            if (!_isActive) return;
            
            var moveDirection = _inputHandler.CalculateMoveDirection();
            var lookDirection = _inputHandler.CalculateLookDirection();
            _movement.Move(moveDirection, _moveSpeed);
            _mouseLook.RotateToLookDirection(lookDirection);
            _shoot.Reload();
            
            if (_inputHandler.IsJump() && _movement.IsJumped(jumpForce)) 
            {
                ResetStats();
                _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.JumpSounds,1f);
                _visualReaction.EffectReact(ServiceLocator.Current.Get<PrefabsStorage>().Storage.JumpEffect,1.5f);
                
                var heal = Mathf.RoundToInt(maxHealth * 0.2f);
                Heal(heal);
                
                _colorChanger.ChangeGradientColor(_health.GetHealth);
                _visualReaction.PopUpReact(_popUpPool, heal.ToString(), heal);
            }
            
            if (_inputHandler.IsDash() && _movement.IsDashed()) 
            {
                ResetStats();
                _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.DashSounds,0.75f);
                _visualReaction.EffectReact(ServiceLocator.Current.Get<PrefabsStorage>().Storage.DashEffect, 1.25f);
                
                var heal = Mathf.RoundToInt(maxHealth * 0.5f);
                Heal(heal);
                
                _colorChanger.ChangeGradientColor(_health.GetHealth);
                _visualReaction.PopUpReact(_popUpPool, heal.ToString(), heal);
            }
            
            if (_inputHandler.IsShoot() && _shoot.IsShoot(_damage)) 
            {
                ChangeLaserLength();
                DecreaseHealth(_health);
                _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.ShootSounds,0.25f);
            }
        }
        #endregion

        #region public methods

        private void OnStartGame() => _isActive = true;
        private void OnPauseGame() => _isActive = false;
        private void OnResumeGame() => _isActive = true;
        private void OnFinishGame() => _isActive = false;
        public void Heal(int amount) => _health.Increase(amount);

        #endregion
        
        #region private methods
        
        private void Enable() {
            _health.OnHealthChanged += _colorChanger.ChangeGradientColor;
            _movement.OnLanded += OnLandReact;

            EventBus.Instance.OnCriticalShot += OnCriticalDamageReact;
            EventBus.Instance.OnGameStart += OnStartGame;
            EventBus.Instance.OnGamePause += OnPauseGame;
            EventBus.Instance.OnGameResume += OnResumeGame;
            EventBus.Instance.OnGameFinish += OnFinishGame;

            StartCoroutine(UpdatePlayerPositionData());
        }
        
        private void Dispose() {
            _health.OnHealthChanged -= _colorChanger.ChangeGradientColor;
            _movement.OnLanded -= OnLandReact;

            EventBus.Instance.OnCriticalShot -= OnCriticalDamageReact;

            EventBus.Instance.OnGameStart -= OnStartGame;
            EventBus.Instance.OnGamePause -= OnPauseGame;
            EventBus.Instance.OnGameResume -= OnResumeGame;
            EventBus.Instance.OnGameFinish -= OnFinishGame;

            StopCoroutine(UpdatePlayerPositionData());
        }

        private void ChangeLaserLength() {
            if (_shoot.HitPoint != Vector3.zero) 
            {
                var distance = Vector3.Distance(transform.position, _shoot.HitPoint);
                var length = distance * 0.02f;
                _laserContainer.localScale = new Vector3(1, 1, length);
            }
            else _laserContainer.localScale = Vector3.one;
        }
        
        private void OnCriticalDamageReact() {
            var randomPhrase = ServiceLocator.Current.Get<PrefabsStorage>().Storage.CritPhrases.GetRandom();
            _visualReaction.PopUpReact(_popUpPool, randomPhrase, 10);
            _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.CriticalShotSounds, 0.5f);
        }
        
        private void ResetStats() {
            _moveSpeed = startMoveSpeed;
            _damage = startDamage;
            _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.HealSounds,0.2f);
        }
        
        private void DecreaseHealth(Health health) {
            health.Reduce(_damage);
            if (health.GetHealth < 1) 
            {
                _moveSpeed = startMoveSpeed * 0.5f;
                _damage = startDamage * damageMultiplyerKoef;
            }
        }
        
        private void OnLandReact() {
            _visualReaction.EffectReact(ServiceLocator.Current.Get<PrefabsStorage>().Storage.LandingEffect,2f);
            _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.LandSounds, 1f);
           EventBus.Instance.OnLanded?.Invoke();
        }
        
        private IEnumerator UpdatePlayerPositionData() 
        {
            while (this) 
            {
                yield return new WaitForSeconds(0.5f);
                EventBus.Instance.OnUpdatePlayerPositionData?.Invoke(transform.position);
            }
        }

        #endregion
    }
}
