using _gitProject.logic.Player;
using UnityEngine;

namespace _gitProject.logic.ServiceLocator {
    public class ServiceLocatorGame : MonoBehaviour {

        [SerializeField] private PlayerController _playerController;
        private void Awake() {
            Register();
            Init();
        }

        private void Register() {
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(_playerController);
        }

        private void Init() {
            _playerController.Init();
        }
    }
}
