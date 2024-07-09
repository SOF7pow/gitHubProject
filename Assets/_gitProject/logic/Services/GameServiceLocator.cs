using _gitProject.logic.ScriptableObjects;
using UnityEngine;

namespace _gitProject.logic.Services {
    public class GameServiceLocator : MonoBehaviour {

        [SerializeField] private SoundsStorageScriptableObject _soundsStorage;
        [SerializeField] private PrefabsStorageScriptableObject _prefabsStorage;
        private void Awake() {
            var prefabs = new PrefabsData(_prefabsStorage);
            var sounds = new SoundsData(_soundsStorage);
            
            var player = Instantiate(prefabs.Storage.PlayerController, Vector3.up * 15f, Quaternion.identity);
            var cameraFollow = Instantiate(prefabs.Storage.CameraBehaviour);
            
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(prefabs);
            ServiceLocator.Current.Register(sounds);
            ServiceLocator.Current.Register(player);
            ServiceLocator.Current.Register(cameraFollow);
            
            player.Initialize();
            cameraFollow.Initialize(player.transform);
            
            SpawnFirstWaveTest(prefabs);
            
        }
        private void SpawnFirstWaveTest(PrefabsData data) {
            for (var i = 0; i < 25; i++) {
                var enemy = Instantiate(data.Storage.enemyBehaviour);
                enemy.Initialize();
            }
        }
    }
}
