using _gitProject.logic.Player;
using UnityEngine;

namespace _gitProject.logic.ViewCamera {
    public class CameraFollow : MonoBehaviour {
        public Transform target;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;
        
        private void Start() {
            target = ServiceLocator.ServiceLocator.Current.Get<PlayerController>().transform;
        }
        
        private void LateUpdate() {
            if (!target) return;
            var scroll = Input.mouseScrollDelta.y * 1.1f;
            offset.y = Mathf.Clamp(offset.y - scroll, 5, 25);
            
            var desiredPosition = target.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }
    }
}
