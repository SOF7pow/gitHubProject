using _gitProject.logic.Events;
using UnityEngine;

namespace _gitProject.logic.UI.Panels {
    public sealed class MenuSwitcher : MonoBehaviour {

        #region fields

        private CanvasGroup _canvasGroup;

        #endregion

        #region initialization

        public void Init() {
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
            
            Enable();
            ShowPanel();
        }

        #endregion

        #region unity callbacks

        private void OnDisable() => Dispose();

        #endregion

        #region private methods

        private void Enable() {
            EventBus.Instance.OnGamePause += ShowPanel;
            EventBus.Instance.OnGameResume += HidePanel;
        }
        private void Dispose() {
            EventBus.Instance.OnGamePause -= ShowPanel;
            EventBus.Instance.OnGameResume -= HidePanel;
        }

        private void ShowPanel() {
            EventBus.Instance.OnMenu?.Invoke();
            _canvasGroup.alpha = 1;
        }
        
        private void HidePanel() {
            EventBus.Instance.OnMenu?.Invoke();
            _canvasGroup.alpha = 0;
        }

        #endregion
    }
}
