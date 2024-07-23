using _gitProject.logic.Services;
using UnityEngine;

namespace _gitProject.logic.StateMachine {
    public class GameController: IService {
        
        private readonly GameStateMachine _gameStateMachine;
        public GameController() => 
            _gameStateMachine = new GameStateMachine();

        #region public methdods

        public void UpdateGameController() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                PauseGame();
            }
        }
        public void StartGame() => _gameStateMachine.StartGame();
        public void PauseGame() => _gameStateMachine.PauseGame();
        public void ResumeGame() => _gameStateMachine.ResumeGame();
        public void FinishGame() => _gameStateMachine.FinishGame();

        #endregion
    }
}
