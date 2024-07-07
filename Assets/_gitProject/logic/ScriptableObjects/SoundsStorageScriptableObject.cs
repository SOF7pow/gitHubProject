using UnityEngine;

namespace _gitProject.logic.ScriptableObjects {
    [CreateAssetMenu(menuName = "SoundsStorageSO")]
    public class SoundsStorageScriptableObject : ScriptableObject {
        
        public AudioClip[] Hit;
        public AudioClip[] Shoot;
        public AudioClip[] Die;
        public AudioClip[] CriticalShot;
        public AudioClip[] Dash;
        public AudioClip[] Jump;
    }
}
