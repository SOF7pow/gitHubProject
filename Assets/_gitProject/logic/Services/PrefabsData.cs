using _gitProject.logic.ScriptableObjects;
using UnityEngine;

namespace _gitProject.logic.Services {
    public sealed class PrefabsData : IService {
        public PrefabsStorageScriptableObject Storage { get; }
        public PrefabsData() {
            Storage = Resources.Load<PrefabsStorageScriptableObject>("SO/PrefabStorageSO");
        }
    }
}
