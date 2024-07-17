using _gitProject.logic.Services;
using UnityEngine;
using EventBus = _gitProject.logic.Events.EventBus;

namespace _gitProject.logic.StateMachine {
    public class GameController: IService {
        
        private readonly GameStateMachine _gameStateMachine;
        public GameController() {
            _gameStateMachine = new GameStateMachine();
        }
        public void UpdateGameController() {
            if (Input.GetKeyDown(KeyCode.Escape))
                _gameStateMachine.PauseGame();
        }
        public void StartGame() {
            EventBus.Instance.OnGameStart?.Invoke();
            _gameStateMachine.StartGame();
        }
        public void PauseGame() {
            EventBus.Instance.OnGamePause?.Invoke();
            _gameStateMachine.PauseGame();
        }
        public void ResumeGame() {
            EventBus.Instance.OnGameResume?.Invoke();
            _gameStateMachine.ResumeGame();
        }
        public void FinishGame() {
            EventBus.Instance.OnGameFinish?.Invoke();
            _gameStateMachine.FinishGame();
        }

    }
}
