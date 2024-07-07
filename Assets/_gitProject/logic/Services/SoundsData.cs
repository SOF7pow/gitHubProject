using _gitProject.logic.ScriptableObjects;
using UnityEngine;

namespace _gitProject.logic.Services {
    public class SoundsData : IService {
        public SoundsStorageScriptableObject Storage { get; }
        public SoundsData() {
            Storage = Resources.Load<SoundsStorageScriptableObject>("SO/SoundStorageSO");
        }
    }
}


        

