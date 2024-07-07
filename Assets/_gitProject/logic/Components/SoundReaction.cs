using _gitProject.logic.Helper;
using UnityEngine;

namespace _gitProject.logic.Components {
    public class SoundReaction {
        private readonly AudioSource _source;
        public SoundReaction(AudioSource source) {
            _source = source;
        }
        public void React(AudioClip[] clips, float volume) {
            _source.PlayOneShot(clips.GetRandom(), volume);
        }
    }
}
