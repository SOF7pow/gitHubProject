using _gitProject.logic.Events;
using _gitProject.logic.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _gitProject.logic.ViewCamera {
    public class CameraBehaviour : MonoBehaviour, IService {

        [SerializeField] private float maxOffsetYDistance;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private Vector3 offset;
        [Space]
        [Header("Shaker")]
        [SerializeField] private float shakeDurationValue = 0.1f;
        [SerializeField] private float shakePosValue = 0.05f;
        [SerializeField] private float shakeRotValue = 0.05f;
        
        private Transform _target;
        private Transform _child;
        public void Initialize(Transform target) {
            _target = target;
            _child = GetComponentInChildren<Camera>().transform;
            EventBus.Instance.OnCriticalShot += ShakeCamera;
        }
        private void OnDisable() {
            EventBus.Instance.OnCriticalShot += ShakeCamera;
        }
        private void LateUpdate() {
            Follow(_target);
        }
        private void Follow(Transform target) {
            if (!target) return;
            var scroll = Input.mouseScrollDelta.y * 1.1f;
            offset.y = Mathf.Clamp(offset.y - scroll, 5, maxOffsetYDistance);
            
            var desiredPosition = target.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }
        private void ShakeCamera() {
            _child.transform.DOShakePosition(shakeDurationValue, shakePosValue);
            _child.transform.DOShakeRotation(shakeDurationValue, shakeRotValue);
        }
    }
}
