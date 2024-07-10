using System.Collections;
using _gitProject.logic.Components;
using _gitProject.logic.Components.Labels;
using _gitProject.logic.Events;
using _gitProject.logic.Helper;
using _gitProject.logic.ObjectsPool;
using _gitProject.logic.Services;
using _gitProject.logic.Storage;
using _gitProject.logic.ViewCamera;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace _gitProject.logic.Player {
    
    [RequireComponent(typeof(CharacterController),typeof(AudioSource))]
    public class PlayerController : MonoBehaviour, IService {

        private GameObjectPool _popUpPool;
        
        private Shoot _shoot;
        private Health _health;
        private Movement _movement;
        private MouseLook _mouseLook;
        private InputHandler _inputHandler;
        private HealthColorChanger _healthColorChanger;

        private SoundReaction _soundReaction;
        private VisualReaction _visualReaction;
        
        private Transform _muzzle;
        private LaserLabel _laser;
        private Transform _laserContainer;
        
        private float _moveSpeed;
        private int _damage;

        [SerializeField, UnityEngine.Min(1)] private int _maxHealth = 100;
        [SerializeField, UnityEngine.Min(1), Max(20)] private int _startMoveSpeed = 10;
        [SerializeField, UnityEngine.Min(1), Max(20)] private int _startDamage = 1;
        
        [SerializeField,UnityEngine.Min(0)] private float _jumpForce;
        [SerializeField,UnityEngine.Min(0)] private float _rotateSpeed;
        [SerializeField,UnityEngine.Min(0)] private float _fireRate;
        [SerializeField,UnityEngine.Min(1)] private int _damageMultiplyerKoef;
        public void Initialize() {
            var playerContainer = new GameObject().transform;
            _popUpPool = new GameObjectPool(ServiceLocator.Current.Get<PrefabsStorage>().Storage.PopUpDamage, "PlayerContainer",5, playerContainer);
            
            _muzzle ??= GetComponentInChildren<MuzzleLabel>().transform;
            _laserContainer ??= _muzzle.GetComponentInChildren<LaserContainerLabel>().transform;
            _moveSpeed = _startMoveSpeed;
            _damage = _startDamage;
            
            _health = new Health(_maxHealth);
            _movement = new (GetComponent<CharacterController>());
            _healthColorChanger = new HealthColorChanger(_health.GetHealth, GetComponent<Renderer>(), Color.white, Color.red);
            _mouseLook = new (transform, _rotateSpeed);
            _inputHandler = new (transform, ServiceLocator.Current.Get<CameraBehaviour>().GetComponentInChildren<Camera>());
            _shoot = new (_muzzle, _fireRate, _damageMultiplyerKoef);
            _soundReaction = new(GetComponent<AudioSource>());
            _visualReaction = new VisualReaction(this, transform);
            
            EventsStorage.Instance.OnCriticalShot += OnCriticalDamageReact;
            _health.OnHealthChanged += _healthColorChanger.ChangeGradientColor;
            _movement.OnLanded += OnLandReact;
            StartCoroutine(UpdatePlayerPositionData());
        }
        private void OnDisable() {
            EventsStorage.Instance.OnCriticalShot -= OnCriticalDamageReact;
            _health.OnHealthChanged -= _healthColorChanger.ChangeGradientColor;
            _movement.OnLanded -= OnLandReact;
            StopCoroutine(UpdatePlayerPositionData());
        }
        private void Update() {
            var moveDirection = _inputHandler.CalculateMoveDirection();
            var lookDirection = _inputHandler.CalculateLookDirection();
            _movement.Move(moveDirection, _moveSpeed);
            _mouseLook.RotateToLookDirection(lookDirection);
            _shoot.Reload();

            if (_inputHandler.IsJump() && _movement.IsJumped(_jumpForce)) {
                ResetStats();
                _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.JumpSounds,1f);
                _visualReaction.EffectReact(ServiceLocator.Current.Get<PrefabsStorage>().Storage.JumpEffect,1.5f);
                
                var heal = Mathf.RoundToInt(_maxHealth * 0.2f);
                _health.Regenerate(heal);
                _healthColorChanger.ChangeGradientColor(_health.GetHealth);
                
                _visualReaction.PopUpReact(_popUpPool, heal.ToString(), heal);
            }
            
            if (_inputHandler.IsDash() && _movement.IsDashed()) {
                ResetStats();
                _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.DashSounds,0.75f);
                _visualReaction.EffectReact(ServiceLocator.Current.Get<PrefabsStorage>().Storage.DashEffect, 1.25f);
                
                var heal = Mathf.RoundToInt(_maxHealth * 0.5f);
                _health.Regenerate(heal);
                _healthColorChanger.ChangeGradientColor(_health.GetHealth);
                
                _visualReaction.PopUpReact(_popUpPool, heal.ToString(), heal);
            }
            
            if (_inputHandler.IsShoot() && _shoot.IsShoot(_damage)) {
                ChangeLaserLength();
                DecreaseHealth(_health);
                _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.ShootSounds,0.25f);
            }
        }
        private void ChangeLaserLength() {
            if (_shoot.HitPoint != Vector3.zero) {
                var distance = Vector3.Distance(transform.position, _shoot.HitPoint);
                var length = distance * 0.02f;
                _laserContainer.localScale = new Vector3(1, 1, length);
            }
            else _laserContainer.localScale = Vector3.one;
        }
        private void OnCriticalDamageReact() {
            var phrase = ServiceLocator.Current.Get<PrefabsStorage>().Storage.CritPhrases.GetRandom();
            _visualReaction.PopUpReact(_popUpPool,phrase, 10);
            _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.CriticalShotSounds, 0.5f);
        }
        private void ResetStats() {
            _moveSpeed = _startMoveSpeed;
            _damage = _startDamage;
            _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.HealSounds,0.2f);
        }
        private void DecreaseHealth(Health health) {
            health.Reduce(_damage);
            if (health.GetHealth < 1) {
                _moveSpeed = _startMoveSpeed * 0.5f;
                _damage = _startDamage * _damageMultiplyerKoef;
            }
        }
        private void OnLandReact() {
            _visualReaction.EffectReact(ServiceLocator.Current.Get<PrefabsStorage>().Storage.LandingEffect,2f);
            _soundReaction.React(ServiceLocator.Current.Get<SoundsStorage>().Storage.LandSounds, 1f);
            EventsStorage.Instance.OnLanded?.Invoke();
        }
        private IEnumerator UpdatePlayerPositionData() 
        {
            while (true) {
                yield return new WaitForSeconds(0.5f);
                EventsStorage.Instance.OnUpdatePlayerPositionData?.Invoke(transform.position);
            }
        }
    }
}
