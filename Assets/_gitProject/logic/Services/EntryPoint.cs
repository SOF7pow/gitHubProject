using _gitProject.logic.Player;
using _gitProject.logic.ViewCamera;
using UnityEngine;

namespace _gitProject.logic.Services {
    public class EntryPoint : MonoBehaviour {

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private CameraFollow _camera;
        private void Awake() {
            Register();
            Init();
        }
        
        private void Register() {
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(_playerController);
            ServiceLocator.Current.Register(_camera);
        }
        private void Init() {
            var player = Instantiate(_playerController, Vector3.up, Quaternion.identity);
            player.Initialize();
            _camera.Initialize(player.transform);
        }
    }
}
