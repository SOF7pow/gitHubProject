using System;
using _gitProject.logic.Player;
using _gitProject.logic.ServiceLocator;
using UnityEngine;

namespace Project.Cubus.Camera.scripts {
    public class CameraFollow : MonoBehaviour {
        public Transform target;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;

        private void Start() {
           target = ServiceLocator.Current.Get<Character>().transform;
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
