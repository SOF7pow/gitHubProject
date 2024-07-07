using System.Collections;
using _gitProject.logic.Components;
using _gitProject.logic.Components.Labels;
using _gitProject.logic.Events;
using _gitProject.logic.Helper;
using _gitProject.logic.Services;
using _gitProject.logic.ViewCamera;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace _gitProject.logic.Player {
    
    [RequireComponent(typeof(CharacterController),typeof(AudioSource))]
    public class PlayerController : MonoBehaviour, IService {

        private Shoot _shoot;
        private Movement _movement;
        private MouseLook _mouseLook;
        private InputHandler _inputHandler;
        private Health _health;
        private ColorChanger _colorChanger;
        
        private SoundsData _soundsData;
        private PrefabsData _prefabData;

        private SoundReaction _soundReaction;
        private VisualReaction _dashVisualReaction;
        private VisualReaction _criticalDamageVisualReaction;
        
        private LaserLabel _laser;
        private Transform _laserContainer;
        private Transform _muzzle;
        private AudioSource _source;
        private Transform _transform;
        private float _moveSpeed;

        [SerializeField, UnityEngine.Min(1)] private int _maxHealth = 100;
        [SerializeField, UnityEngine.Min(1), Max(20)] private int _maxMoveSpeed = 10;
        
        
        
        [SerializeField,UnityEngine.Min(0)] private float _jumpForce;
        [SerializeField,UnityEngine.Min(0)] private float _rotateSpeed;
        [SerializeField,UnityEngine.Min(0.01f)] private float _fireRate;
        [SerializeField,UnityEngine.Min(1)] private int _damage;
        [SerializeField,UnityEngine.Min(1)] private int _damageMultiplyerKoef;
        public void Initialize() {
            _transform ??= GetComponent<Transform>();
            _muzzle ??= GetComponentInChildren<MuzzleLabel>().transform;
            _laser ??= GetComponentInChildren<LaserLabel>();
            _laserContainer ??= _laser.GetComponentInParent<LaserContainerLabel>().transform;
            _source ??= GetComponent<AudioSource>();
            _soundsData = ServiceLocator.Current.Get<SoundsData>();
            _prefabData = ServiceLocator.Current.Get<PrefabsData>();

            _moveSpeed = _maxMoveSpeed;
            _health = new Health(_maxHealth);
            _colorChanger = new ColorChanger(_health.GetHealth, GetComponent<Renderer>(), Color.white, Color.red);
            
            _mouseLook = new (_transform, _rotateSpeed);
            _movement = new (GetComponent<CharacterController>());
            _inputHandler = new (_transform, ServiceLocator.Current.Get<CameraBehaviour>().GetComponentInChildren<Camera>());
            _shoot = new (_muzzle, _laser, _fireRate, _damageMultiplyerKoef);
            _soundReaction = new(_source);
            
            _dashVisualReaction = new EffectVisualReaction(_transform);
            _criticalDamageVisualReaction = new PopUpVisualReaction(_prefabData.Storage.PopUpDamage, _transform);
            
            EventBus.Instance.OnCriticalShot += OnCriticalDamageReact;
            _health.OnHealthChanged += _colorChanger.ChangeGradientColor;
            StartCoroutine(UpdatePlayerPositionData());
        }
        
        private void OnDisable() {
            EventBus.Instance.OnCriticalShot -= OnCriticalDamageReact;
            _health.OnHealthChanged -= _colorChanger.ChangeGradientColor;
            StopCoroutine(UpdatePlayerPositionData());
        }

        private void Update() {
            var moveDirection = _inputHandler.CalculateMoveDirection();
            var lookDirection = _inputHandler.CalculateLookDirection();
            
            _movement.Move(moveDirection, _moveSpeed);
            _mouseLook.RotateToLookDirection(lookDirection);
            _shoot.ShootCoolDown();

            if (_inputHandler.IsJump() && _movement.IsJumped(_jumpForce)) {
                _soundReaction.React(_soundsData.Storage.Jump,1f);
                _dashVisualReaction.React(0.5f,_prefabData.Storage.DashEffect);
                var heal = Mathf.RoundToInt(_maxHealth / 5);
                _health.Regenerate(heal);
                _colorChanger.ChangeGradientColor(_health.GetHealth);
                _criticalDamageVisualReaction.React(heal.ToString(), _prefabData.Storage.PopUpDamage);
            }
            
            if (_inputHandler.IsShoot() && _shoot.IsShooted(_damage)) {
                ChangeLaserLength();
                _soundReaction.React(_soundsData.Storage.Shoot,0.25f);
                _health.Reduce(_damage);
                if (_health.GetHealth < 1) _moveSpeed = 1;
            }
            
            if (_inputHandler.IsDash() && _movement.Dashing()) {
                _soundReaction.React(_soundsData.Storage.Dash,0.75f);
                _dashVisualReaction.React(2f,_prefabData.Storage.DashEffect);
                var heal = Mathf.RoundToInt(_maxHealth / 2);
                _health.Regenerate(heal);
                _colorChanger.ChangeGradientColor(_health.GetHealth);
                _criticalDamageVisualReaction.React(heal.ToString(), _prefabData.Storage.PopUpDamage);
                _moveSpeed = _maxMoveSpeed;
            }
        }
        
        private void ChangeLaserLength() {
            if (_shoot.HitPoint != Vector3.zero) {
                var distance = Vector3.Distance(transform.position, _shoot.HitPoint);
                var length = distance / 50;
                _laserContainer.localScale = new Vector3(1, 1, length);
            }
            else 
                _laserContainer.localScale = new Vector3(1, 1, 1);
        }

        private void OnCriticalDamageReact() {
            var phrase = _prefabData.Storage.CritPhrases.GetRandom();
            _criticalDamageVisualReaction.React(phrase,_prefabData.Storage.PopUpDamage);
            _soundReaction.React(_soundsData.Storage.CriticalShot, 0.5f);
        }

        private IEnumerator UpdatePlayerPositionData() 
        {
            while (true) {
                yield return new WaitForSeconds(0.25f);
                EventBus.Instance.OnUpdatePlayerPosition?.Invoke(transform.position);
            }
        }
    }
}
