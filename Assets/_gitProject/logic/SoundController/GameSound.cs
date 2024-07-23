using _gitProject.logic.Events;
using _gitProject.logic.ExtensionMethods;
using _gitProject.logic.Storage;
using UnityEngine;

namespace _gitProject.logic.SoundController {
    public class GameSound {

        #region fields

        private readonly SoundsStorage _soundsStorage;
        private AudioSource _audioSource;

        #endregion

        #region constructor

        public GameSound(SoundsStorage soundsStorage) 
        {
            _soundsStorage = soundsStorage;
            
            Enable();   
        }

        #endregion

        #region public methods

        public void Dispose() {
            EventBus.Instance.OnMenu -= PlaySoundMenu;
            EventBus.Instance.OnMenuButtonClick -= PlaySoundMenu;
        }

        #endregion

        #region private methods

        private void Enable() {
            EventBus.Instance.OnMenu += PlaySoundMenu;
            EventBus.Instance.OnMenuButtonClick += PlaySoundMenu;
        }
        private void PlaySoundMenu() => AudioSource.PlayClipAtPoint(
            _soundsStorage.Storage.HitSounds.GetRandom(),Vector3.zero);

        #endregion
    }
}
