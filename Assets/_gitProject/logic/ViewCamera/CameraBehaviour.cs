using _gitProject.logic.Events;
using _gitProject.logic.Services;
using DG.Tweening;
using UnityEngine;

namespace _gitProject.logic.ViewCamera {
    public class CameraBehaviour : MonoBehaviour, IService {

        #region fields

        private Transform _target;
        private Transform _child;
        [Header("Shaker settings")]
        [SerializeField] private float shakeDurationValue = 0.1f;
        [SerializeField] private float shakePosValue = 0.05f;
        [SerializeField] private float shakeRotValue = 0.05f;
        [Space]
        [Header("Camera settings")]
        [SerializeField] private float maxOffsetYDistance;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private Vector3 offset;

        #endregion

        #region initialization

        public void Initialize(Transform target) {
            _target = target;
            _child = GetComponentInChildren<Camera>().transform;
            
            EventBus.Instance.OnCriticalShot += ShakeCamera;
            EventBus.Instance.OnLanded += ShakeCamera;
        }

        #endregion

        #region unity callbacks

        private void LateUpdate() => Follow(_target);
        private void OnDisable() {
            EventBus.Instance.OnCriticalShot -= ShakeCamera;
            EventBus.Instance.OnLanded -= ShakeCamera;
        }

        #endregion

        #region private methods

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

        #endregion
    }
}
