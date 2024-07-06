using UnityEngine;

namespace _gitProject.logic.Services {
    public class SoundStorage : MonoBehaviour, IService {

        [SerializeField] private AudioClip[] _hitClips;
        [SerializeField] private AudioClip[] _shootClips;
        [SerializeField] private AudioClip[] _dieClips;
        
        public AudioClip[] HitClips => _hitClips;
        public AudioClip[] ShootClips => _shootClips;

        public AudioClip[] DieClips => _dieClips;
    }
}
