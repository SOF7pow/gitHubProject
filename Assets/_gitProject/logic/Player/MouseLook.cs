using _gitProject.logic.EventsLocator;
using UnityEngine;

namespace _gitProject.logic.Player {
    public class MouseLook : MonoBehaviour {
        
        private Transform _transform;
        
        [SerializeField] private float _rotateSpeed;
        private void Awake() => _transform = GetComponent<Transform>();
        private void OnEnable() => EventLocator.Instance.OnMouseLook += RotateToAimDirection;
        private void OnDisable() => EventLocator.Instance.OnMouseLook -= RotateToAimDirection;
        private void RotateToAimDirection(Vector3 direction) {
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var rotateStep = _rotateSpeed * Time.deltaTime;
            _transform.rotation =
                Quaternion.Slerp(_transform.rotation, Quaternion.Euler(0, angle, 0),rotateStep);
        }
    }
}
