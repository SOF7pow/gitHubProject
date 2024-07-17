using System;
using _gitProject.logic.Events;
using _gitProject.logic.ObjectsPool;
using _gitProject.logic.ScriptableObjects;
using _gitProject.logic.Services;
using _gitProject.logic.SoundController;
using _gitProject.logic.StateMachine;
using _gitProject.logic.Storage;
using UnityEngine;

namespace _gitProject.logic.EntryPoint {
    public class GameLauncher : MonoBehaviour {
        
        #region fields

        [SerializeField] private SoundsStorageSO _soundsStorage;
        [SerializeField] private PrefabsStorageSO _prefabsStorage;
        
        private GameController _gameController;
        private GameObjectPool _popUpPool;
        private GameObjectPool _hitEffectPool;
        private PrefabsStorage _prefabs;
        private SoundsStorage _sounds;

        private GameSound _gameSound;

        #endregion

        #region initialization

        private void Awake() {
            _prefabs = new PrefabsStorage(_prefabsStorage);
            _sounds = new SoundsStorage(_soundsStorage);
            _popUpPool = new GameObjectPool(_prefabsStorage.PopUpDamage, "PopUpContainer", 
                new GameObject().transform, 5);
            _hitEffectPool = new GameObjectPool(_prefabsStorage.BaseHitEffect,"HitEffectsContainer", 
                new GameObject().transform, 10);
            _gameController = new GameController();

            _gameSound = new GameSound(EventBus.Instance, _sounds);
            
            var player = Instantiate(_prefabs.Storage.PlayerController, Vector3.up * 5f, Quaternion.identity);
            var cameraFollow = Instantiate(_prefabs.Storage.CameraBehaviour);

            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(_gameController);
            ServiceLocator.Current.Register(_prefabs);
            ServiceLocator.Current.Register(_sounds);
            ServiceLocator.Current.Register(player);
            ServiceLocator.Current.Register(cameraFollow);

            player.Initialize();
            cameraFollow.Initialize(player.transform);
            SpawnFirstWaveTest(_prefabs);
            
            _gameController.StartGame();
        }

        private void Update() => _gameController.UpdateGameController();

        private void OnDisable() {
            _gameSound.Dispose();
        }

        #endregion
        private void SpawnFirstWaveTest(PrefabsStorage storage) {
            for (var i = 0; i < 10; i++) {
                var enemy = Instantiate(storage.Storage.EnemyBehaviour);
                enemy.Initialize(_popUpPool, _hitEffectPool, _sounds);
            }
        }
    }
}
