using _gitProject.logic.Enemies;
using _gitProject.logic.Player;
using _gitProject.logic.ViewCamera;
using UnityEngine;
using UnityEngine.Serialization;

namespace _gitProject.logic.ScriptableObjects {
    [CreateAssetMenu(menuName = "PrefabsStorageSO")]
    public class PrefabsStorageScriptableObject : ScriptableObject {
        
        [Header("Logic")]
        public CameraBehaviour CameraBehaviour;
        public PlayerController PlayerController;
        public EnemyController EnemyController;
        
        [FormerlySerializedAs("PopUpDamageInfo")] [Space, Header("Effects")]
        public GameObject PopUpDamage;

        public GameObject BaseHitEffect;
        public GameObject DeathEffect;
        public GameObject DashEffect;
        public GameObject JumpEffect;
        public GameObject LandingEffect;
        
        [Space, Header("Phrases")]
        public string[] CritPhrases = {"crit", "wow", "boom","!!!"};
    }
}
