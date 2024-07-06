using UnityEngine;

namespace _gitProject.logic.Services {
    public class EntryPoint : MonoBehaviour {

        [SerializeField] private SoundStorage _soundStorage;
        [SerializeField] private PrefabStorage _prefabStorage;
        
        private void Awake() {
            _soundStorage ??= GetComponentInChildren<SoundStorage>();
            _prefabStorage ??= GetComponentInChildren<PrefabStorage>();
            var player = Instantiate(_prefabStorage.PlayerController);
            var cameraFollow = Instantiate(_prefabStorage.Camera);
            
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(player);
            ServiceLocator.Current.Register(cameraFollow);
            ServiceLocator.Current.Register(_soundStorage);
            ServiceLocator.Current.Register(_prefabStorage);
            
            player.Initialize();
            cameraFollow.Initialize(player.transform);
            
            for (var i = 0; i < 25; i++) {
                var enemy = Instantiate(_prefabStorage.EnemyController);
                enemy.Initialize();
            }
        }
    }
}
