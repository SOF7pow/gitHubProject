using _gitProject.logic.Services;
using UnityEngine;

namespace _gitProject.logic.ViewCamera {
    public class CameraFollow : MonoBehaviour, IService {
        private Transform _target;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;
        
        public void Initialize(Transform target) => _target = target;

        private void LateUpdate() {
            if (!_target) return;
            var scroll = Input.mouseScrollDelta.y * 1.1f;
            offset.y = Mathf.Clamp(offset.y - scroll, 5, 25);
            
            var desiredPosition = _target.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(_target);
        }
    }
}
