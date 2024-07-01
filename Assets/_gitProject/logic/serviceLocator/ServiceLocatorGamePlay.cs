using UnityEngine;
namespace _gitProject.logic {
    public class ServiceLocatorGamePlay : MonoBehaviour {
        [SerializeField] private Player _player;



        private void Awake() {
            RegisterServices();
            Init();
        }
    }
}
