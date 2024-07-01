using _gitProject.logic.Player;
using UnityEngine;

namespace _gitProject.logic.ServiceLocator {
    public class ServiceLocatorGame : MonoBehaviour {

        [SerializeField] private Character _character;
        private void Awake() {
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(_character);
        }
    }
}
