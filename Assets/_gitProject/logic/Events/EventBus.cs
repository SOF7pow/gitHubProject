using System;
using UnityEngine;

namespace _gitProject.logic.Events {
    public sealed class EventBus {
        public EventBus() {}

        private static EventBus _instance;
        public static EventBus Instance => _instance ??= new EventBus();

        public Action OnGameStart { get; set; }
        
        public Action OnGamePause { get; set; }
        public Action OnGameResume { get; set; }
        public Action OnGameFinish { get; set; }
        public Action OnMenu { get; set; }
        
        public Action OnMenuButtonClick { get; set; }
        
        public Action<Vector3> OnUpdatePlayerPositionData { get; set; }
        
        public Action OnCriticalShot { get; set; }
        
        public Action OnLanded { get; set; }
    }
}
