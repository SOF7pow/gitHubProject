using _gitProject.logic.ScriptableObjects;

namespace _gitProject.logic.Services {
    public sealed class PrefabsData : IService {
        
        private PrefabsStorageScriptableObject _storage;
        public PrefabsStorageScriptableObject Storage => _storage;
        public PrefabsData(PrefabsStorageScriptableObject storage) => _storage = storage;
    }
}
