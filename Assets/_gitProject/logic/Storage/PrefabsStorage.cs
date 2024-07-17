using _gitProject.logic.ScriptableObjects;
using _gitProject.logic.Services;

namespace _gitProject.logic.Storage {
    public sealed class PrefabsStorage : IService {
        public PrefabsStorage(PrefabsStorageSO storage) => Storage = storage;
        public PrefabsStorageSO Storage { get; }
    }
}
