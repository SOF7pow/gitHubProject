using _gitProject.logic.ScriptableObjects;
using _gitProject.logic.Services;

namespace _gitProject.logic.Storage {
    public sealed class SoundsStorage : IService {
        public SoundsStorage(SoundsStorageSO storage) => Storage = storage;
        public SoundsStorageSO Storage { get; }
    }
}


        

