using System;
using UnityEngine;

namespace _gitProject.logic.Events {
    public class EventBus {
        private EventBus() {}
        private static EventBus _instance;
        public static EventBus Instance => _instance ??= new EventBus();

        public Action OnGameStarted;
        public Action OnGamePaused;

        public Action<Vector3> OnUpdatePlayerPosition;
    }
}
