using _gitProject.logic.EventsLocator;
using UnityEngine;

namespace _gitProject.logic.Player {
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour {

        private CharacterController _characterController;
        [SerializeField] private float moveSpeed = 10f;
        private void Awake() {
            _characterController = GetComponent<CharacterController>();
        }
        private void OnEnable() {
            EventLocator.Instance.OnMoveInput += Move;
        }
        private void OnDisable() {
            EventLocator.Instance.OnMoveInput -= Move;
        }
        private void Move(Vector3 direction) {
            _characterController.Move(direction * (moveSpeed * Time.deltaTime));
        }
    }
}
