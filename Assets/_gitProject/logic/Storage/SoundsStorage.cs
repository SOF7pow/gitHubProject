using _gitProject.logic.ScriptableObjects;
using _gitProject.logic.Services;

namespace _gitProject.logic.Storage {
    public sealed class SoundsStorage : IService {
        
        private SoundsStorageScriptableObject _storage;
        public SoundsStorageScriptableObject Storage => _storage;
        public SoundsStorage(SoundsStorageScriptableObject storage) => _storage = storage;
    }
}


        

