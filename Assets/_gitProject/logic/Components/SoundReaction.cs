using _gitProject.logic.Helper;
using UnityEngine;

namespace _gitProject.logic.Components {
    public class SoundReaction {
        private Transform _transform;
        private readonly AudioSource _source;
        private readonly AudioClip[] _hitClips;
        public SoundReaction(AudioClip[] clips, AudioSource source) {
            _hitClips = clips;
            _source = source;
        }
        public void React() => _source.PlayOneShot(_hitClips.GetRandom());
    }
}
