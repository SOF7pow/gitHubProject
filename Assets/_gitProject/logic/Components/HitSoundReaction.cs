using _gitProject.logic.Extensions;
using UnityEngine;

namespace _gitProject.logic.Components {
    public class HitSoundReaction {

        private AudioClip[] _clips;
        private AudioSource _source;
        
        public HitSoundReaction(AudioClip[] clips, AudioSource source) {
            _clips = clips;
            _source = source;
        }
        public void PlayReactionSound() {
            _source.PlayOneShot(_clips.GetRandom());
        } 
    }
}
