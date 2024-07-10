using _gitProject.logic.ScriptableObjects;
using _gitProject.logic.Services;

namespace _gitProject.logic.Storage {
    public sealed class PrefabsStorage : IService {
        
        private PrefabsStorageScriptableObject _storage;
        public PrefabsStorageScriptableObject Storage => _storage;
        public PrefabsStorage(PrefabsStorageScriptableObject storage) => _storage = storage;
    }
}
