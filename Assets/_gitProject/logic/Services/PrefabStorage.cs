using _gitProject.logic.Enemies;
using _gitProject.logic.Player;
using _gitProject.logic.ViewCamera;
using UnityEngine;

namespace _gitProject.logic.Services {
    public class PrefabStorage : MonoBehaviour, IService {
        public GameObject PopUpDamage;
        public CameraFollow Camera;
        public PlayerController PlayerController;
        public EnemyController EnemyController;
    }
}
