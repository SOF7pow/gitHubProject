using _gitProject.logic.Events;
using UnityEngine;

namespace _gitProject.logic.UI.Panels {
    public sealed class MenuSwitcher : MonoBehaviour {
        private CanvasGroup _canvasGroup;

        private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();
        private void OnEnable() {
            EventBus.Instance.OnGamePause += ShowPanel;
            EventBus.Instance.OnGameResume += HidePanel;
        }
        private void OnDisable() {
            EventBus.Instance.OnGameResume -= HidePanel;
            EventBus.Instance.OnGamePause -= ShowPanel;
        }
        private void ShowPanel() {
            EventBus.Instance.OnMenu?.Invoke();
            _canvasGroup.alpha = 1;
        }

        private void HidePanel() {
            EventBus.Instance.OnMenu?.Invoke();
            _canvasGroup.alpha = 0;
        }

    }
}
