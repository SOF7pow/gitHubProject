using _gitProject.logic.Events;
using UnityEngine;

namespace _gitProject.logic.StateMachine {
    public enum GameState {
        OFF = 0,
        PLAY = 1,
        PAUSE = 2,
        FINISH = 3,
    }
    public sealed class GameStateMachine {
        private GameState GameState { get; set; } = GameState.OFF;
        public void StartGame() {
            if (GameState != GameState.OFF) 
            {
                Debug.Log($"You can start game only from {GameState.PLAY}");
                return;
            }
            GameState = GameState.PLAY;
            Debug.Log("Game is Started");
            EventBus.Instance.OnGameStart?.Invoke();
        }
        public void PauseGame() 
        {
            if (GameState != GameState.PLAY)
            {
                Debug.LogWarning($"You can pause game only from {GameState.PLAY} state!");
                return;
            }

            GameState = GameState.PAUSE;
            Debug.Log("Game is Paused");
            EventBus.Instance.OnGamePause?.Invoke();
        }
        
        public void ResumeGame() 
        {
            if (GameState != GameState.PAUSE) 
            {
                Debug.LogWarning($"You can resume game only from {GameState.PAUSE} state!");
                return;
            }

            GameState = GameState.PLAY;
            Debug.Log("Game is Resumed");
            EventBus.Instance.OnGameResume?.Invoke();
        }
        public void FinishGame() {
            if (GameState != GameState.PAUSE) 
            {
                Debug.LogWarning($"You can finish game only from {GameState.PAUSE} state!");
                return;
            }
            Debug.Log("Game is Run!!! - maybe restart))");
            GameState = GameState.FINISH;
            EventBus.Instance.OnGameFinish?.Invoke();
        }
    }
}
