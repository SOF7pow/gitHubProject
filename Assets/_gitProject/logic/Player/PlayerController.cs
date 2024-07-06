using System.Collections;
using _gitProject.logic.Components;
using _gitProject.logic.Components.Labels;
using _gitProject.logic.Events;
using _gitProject.logic.Services;
using UnityEngine;

namespace _gitProject.logic.Player {
    
    [RequireComponent(typeof(CharacterController),typeof(AudioSource))]
    public class PlayerController : MonoBehaviour, IService {

        private Shoot _shoot;
        private Movement _movement;
        private MouseLook _mouseLook;
        private InputHandler _inputHandler;
        private SoundReaction _shootSoundReaction;
        
        private CharacterController _controller;
        private MeshRenderer _laser;
        private Transform _muzzle;
        private AudioSource _source;

        [SerializeField] private float _jumpForce;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _fireRate;
        [SerializeField] private int _damage;
        public void Initialize() {
            _controller ??= GetComponent<CharacterController>();
            _muzzle ??= GetComponentInChildren<MuzzleLabel>().transform;
            _laser ??= GetComponentInChildren<LaserLabel>().gameObject.GetComponent<MeshRenderer>();
            _source ??= GetComponent<AudioSource>();
            
            _mouseLook = new (transform, _rotateSpeed);
            _movement = new (_controller, _moveSpeed);
            _inputHandler = new (transform, Camera.main);
            _shoot = new (_muzzle, _laser, _damage, _fireRate);
            _shootSoundReaction = new (ServiceLocator.Current.Get<SoundStorage>().ShootClips, _source);
            
            _shoot.OnShoot += _shootSoundReaction.React;
            StartCoroutine(UpdateData());
        }
        
        private void OnDisable() {
            _shoot.OnShoot -= _shootSoundReaction.React;
            StopCoroutine(UpdateData());
        }

        private void Update() {
            var moveDirection = _inputHandler.CalculateMoveDirection();
            var lookDirection = _inputHandler.CalculateLookDirection();
            
            _movement.Move(moveDirection);
            _mouseLook.RotateToLookDirection(lookDirection);
            
            if (_inputHandler.IsJump()) _movement.Jump(_jumpForce);
            if (_inputHandler.IsShoot()) _shoot.ShootFire();
            
            _shoot.ShootCoolDown();
        }

        private IEnumerator UpdateData() 
        {
            while (true) {
                yield return new WaitForSeconds(0.5f);
                EventBus.Instance.OnUpdatePlayerPosition?.Invoke(transform.position);
            }
        }
    }
}
