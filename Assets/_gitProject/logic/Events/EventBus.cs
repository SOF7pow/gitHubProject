using System;
using UnityEngine;

namespace _gitProject.logic.Events {
    public sealed class EventBus {
        private EventBus() {}
        private static EventBus _instance;
        public static EventBus Instance => _instance ??= new EventBus();

        public Action OnGameStart;
        public Action OnGamePause;
        public Action OnGameResume;
        public Action OnGameFinish;
        public Action OnMenu;
        public Action OnMenuButtonClick;
        public Action<Vector3> OnUpdatePlayerPositionData;
        public Action OnCriticalShot;
        public Action OnLanded;
    }
}
