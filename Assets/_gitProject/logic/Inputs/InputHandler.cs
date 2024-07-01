using _gitProject.logic.EventsLocator;
using _gitProject.logic.Interfaces;
using UnityEngine;

namespace _gitProject.logic.Inputs {
    public class InputHandler : MonoBehaviour, IUpdatable {

        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        private void OnEnable() {
            EventLocator.Instance.OnCustomUpdate += CustomUpdate;
            
        }

        private void OnDisable() {
            EventLocator.Instance.OnCustomUpdate -= CustomUpdate;
        }

        public void CustomUpdate() {
            var moveDirection = CalculateMoveDirection();
            if (moveDirection != Vector3.zero)
                EventLocator.Instance.OnMoveInput?.Invoke(moveDirection);

            var lookDirection = CalculateLookDirection();
            EventLocator.Instance.OnMouseLook?.Invoke(lookDirection);
        }
        private Vector3 CalculateMoveDirection() {
            var horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
            var vertical = UnityEngine.Input.GetAxisRaw("Vertical");
            return new Vector3(horizontal, 0, vertical).normalized;
        }
        
        private Vector3 CalculateLookDirection() {
            var mousePosition =
               _camera.ScreenToWorldPoint(new Vector3(
                    Input.mousePosition.x, 
                    Input.mousePosition.y, 
                    _camera.transform.position.y));
            var direction = mousePosition - transform.position;
            return direction;
        }
    }
}
