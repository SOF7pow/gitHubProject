using _gitProject.logic.ObjectsPool;
using _gitProject.logic.ScriptableObjects;
using _gitProject.logic.Storage;
using UnityEngine;

namespace _gitProject.logic.Services {
    public class GameServiceLocator : MonoBehaviour {

        [SerializeField] private SoundsStorageScriptableObject _soundsStorage;
        [SerializeField] private PrefabsStorageScriptableObject _prefabsStorage;
        private GameObjectPool _popUpPool;
        private PrefabsStorage _prefabs;
        private SoundsStorage _sounds;

        private void Awake() {
            //Creation
            _prefabs = new PrefabsStorage(_prefabsStorage);
            _sounds = new SoundsStorage(_soundsStorage);
            var enemyContainer = new GameObject().transform;
            _popUpPool = new GameObjectPool(_prefabsStorage.PopUpDamage, "EnemyContainer", 5, enemyContainer);
            var player = Instantiate(_prefabs.Storage.PlayerController, Vector3.up * 15f, Quaternion.identity);
            var cameraFollow = Instantiate(_prefabs.Storage.CameraBehaviour);
            //Registration
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(_prefabs);
            ServiceLocator.Current.Register(_sounds);
            ServiceLocator.Current.Register(player);
            ServiceLocator.Current.Register(cameraFollow);
            //Initialization
            player.Initialize();
            cameraFollow.Initialize(player.transform);
            SpawnFirstWaveTest(_prefabs);
        }
        private void SpawnFirstWaveTest(PrefabsStorage storage) {
            for (var i = 0; i < 15; i++) {
                var enemy = Instantiate(storage.Storage.EnemyBehaviour);
                enemy.Initialize(_popUpPool,_sounds,_prefabs);
            }
        }
    }
}
