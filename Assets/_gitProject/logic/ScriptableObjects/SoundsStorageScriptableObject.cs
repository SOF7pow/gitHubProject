using UnityEngine;

namespace _gitProject.logic.ScriptableObjects {
    [CreateAssetMenu(menuName = "SoundsStorageSO")]
    public class SoundsStorageScriptableObject : ScriptableObject {
        
         [SerializeField] private AudioClip[] _hitSounds;
         [SerializeField] private AudioClip[] _shootSounds;
         [SerializeField] private AudioClip[] _dieSounds;
         [SerializeField] private AudioClip[] _criticalShotSounds;
         [SerializeField] private AudioClip[] _dashSounds;
         [SerializeField] private AudioClip[] _jumpSounds;
         [SerializeField] private AudioClip[] _healSounds;
         [SerializeField] private AudioClip[] _landSounds;
         public AudioClip[] HitSounds => _hitSounds;
         public AudioClip[] ShootSounds => _shootSounds;
         public AudioClip[] DieSounds => _dieSounds;
         public AudioClip[] CriticalShotSounds => _criticalShotSounds;
         public AudioClip[] DashSounds => _dashSounds;
         public AudioClip[] JumpSounds => _jumpSounds;
         
         public AudioClip[] HealSounds => _healSounds;
         public AudioClip[] LandSounds => _landSounds;
    }
}
