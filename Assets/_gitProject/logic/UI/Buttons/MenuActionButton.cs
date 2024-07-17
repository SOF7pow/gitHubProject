using _gitProject.logic.Services;
using _gitProject.logic.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace _gitProject.logic.UI.Buttons {
    public enum ActionType {
        RESUME = 0,
        FINISH = 1,
        RESTART = 2,
        SAVE = 3,
        
    }
    public sealed class MenuActionButton : MonoBehaviour {

        [SerializeField] private ActionType actionType;
        private void Start() => GetComponent<Button>().onClick.AddListener(OnButtonClick);
        private void OnButtonClick() {
            switch (actionType) {
                case ActionType.RESUME: ServiceLocator.Current.Get<GameController>().ResumeGame();
                    break;
                case ActionType.FINISH: ServiceLocator.Current.Get<GameController>().FinishGame();
                    break;
            }
        }
    }
}
