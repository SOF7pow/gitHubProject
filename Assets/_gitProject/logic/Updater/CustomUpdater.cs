using _gitProject.logic.EventsLocator;
using UnityEngine;

namespace _gitProject.logic.Updater {
    public class CustomUpdater : MonoBehaviour
    { 
        private void Update() => EventLocator.Instance.OnCustomUpdate?.Invoke();
    }
}
