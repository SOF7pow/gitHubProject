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
            player.Initialize
            (
                new MouseLook(player.transform,10),
                new Movement(player.Controller,5f),
                new InputHandler(player.transform,Camera.main),
                new Shoot(player.Muzzle,1,0.5f)
            );
            _camera.Initialize(player.transform);
        }
    }
}
