using System;
using UnityEngine;

namespace _gitProject.logic.EventsLocator {
    public class EventLocator {
        private EventLocator() {}
    
        private static EventLocator _instance;
        public static EventLocator Instance => _instance ??= new EventLocator();
        //Updater
        public Action OnCustomUpdate;
        //Input keyboard
        public Action<Vector3> OnMoveInput;
        public Action<Vector3> OnMouseLook;

    }
}
