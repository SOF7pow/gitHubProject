using _gitProject.logic.Events;
using _gitProject.logic.ExtensionMethods;
using _gitProject.logic.Storage;
using UnityEngine;

namespace _gitProject.logic.SoundController {
    public class GameSound {
        
        private readonly SoundsStorage _soundsStorage;
        private AudioSource _audioSource;
        private readonly EventBus _eventBus;
        
        public GameSound(EventBus eventBus, SoundsStorage soundsStorage) 
        {
            _eventBus = eventBus;
            _soundsStorage = soundsStorage;
            
            Enable();   
        }
        private void Enable() {
            _eventBus.OnMenu += PlaySoundMenu;
            _eventBus.OnMenuButtonClick += PlaySoundMenu;
        }
        // must be called before the parent container is destroyed
        public void Dispose() {
            _eventBus.OnMenu -= PlaySoundMenu;
            _eventBus.OnMenuButtonClick -= PlaySoundMenu;
        }
        private void PlaySoundMenu() => AudioSource.PlayClipAtPoint(
            _soundsStorage.Storage.HealSounds.GetRandom(),Vector3.zero);
    }
}
