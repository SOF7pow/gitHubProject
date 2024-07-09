using _gitProject.logic.ScriptableObjects;

namespace _gitProject.logic.Services {
    public class SoundsData : IService {
        
        private SoundsStorageScriptableObject _storage;
        public SoundsStorageScriptableObject Storage => _storage;
        public SoundsData(SoundsStorageScriptableObject storage) => _storage = storage;
    }
}


        

