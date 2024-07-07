using UnityEngine;

namespace _gitProject.logic.Services {
    public class EntryPoint : MonoBehaviour {
        private void Awake() {
            var soundsData = new SoundsData();
            var prefabData = new PrefabsData();
            
            var player = Instantiate(prefabData.Storage.PlayerController);
            var cameraFollow = Instantiate(prefabData.Storage.CameraBehaviour);
            
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(prefabData);
            ServiceLocator.Current.Register(soundsData);
            ServiceLocator.Current.Register(player);
            ServiceLocator.Current.Register(cameraFollow);
            
            player.Initialize();
            cameraFollow.Initialize(player.transform);
            
            SpawnFirstWaveTest(prefabData);
            
        }

        private void SpawnFirstWaveTest(PrefabsData data) {
            for (var i = 0; i < 50; i++) {
                var enemy = Instantiate(data.Storage.EnemyController);
                enemy.Initialize();
            }
        }
    }
}
