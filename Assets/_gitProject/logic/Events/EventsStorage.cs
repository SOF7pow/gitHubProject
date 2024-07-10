using System;
using UnityEngine;

namespace _gitProject.logic.Events {
    public sealed class EventsStorage {
        private EventsStorage() {}
        private static EventsStorage _instance;
        public static EventsStorage Instance => _instance ??= new EventsStorage();

        public Action OnGameStarted;
        public Action OnGamePaused;

        public Action<Vector3> OnUpdatePlayerPositionData;
        public Action OnCriticalShot;
        public Action OnLanded;
    }
}
