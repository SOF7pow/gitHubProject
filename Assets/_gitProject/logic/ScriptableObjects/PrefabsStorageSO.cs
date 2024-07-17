using _gitProject.logic.EnemyLogic;
using _gitProject.logic.Player;
using _gitProject.logic.ViewCamera;
using UnityEngine;

namespace _gitProject.logic.ScriptableObjects {
    [CreateAssetMenu(menuName = "PrefabsStorageSO")]
    public sealed class PrefabsStorageSO : ScriptableObject {

        #region fields

        [Header("Logic")]
        [SerializeField] private CameraBehaviour cameraBehaviour;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private EnemyBehaviour enemyBehaviour;
        
        [Space, Header("Effects")]
        [SerializeField] private GameObject popUpDamage;
        [SerializeField] private GameObject baseHitEffect;
        [SerializeField] private GameObject deathEffect;
        [SerializeField] private GameObject dashEffect;
        [SerializeField] private GameObject jumpEffect;
        [SerializeField] private GameObject landingEffect;
        
        [Space, Header("Phrases")]
        [SerializeField] private string[] critPhrases = {"crit", "wow", "boom","!!!"};

        #endregion

        #region properties

        public CameraBehaviour CameraBehaviour => cameraBehaviour;
        public PlayerController PlayerController => playerController;
        public EnemyBehaviour EnemyBehaviour => enemyBehaviour;
        public GameObject PopUpDamage => popUpDamage;
        public GameObject BaseHitEffect => baseHitEffect;
        
        public GameObject DeathEffect => deathEffect;
        public GameObject DashEffect => dashEffect;
        public GameObject JumpEffect => jumpEffect;
        public GameObject LandingEffect => landingEffect;
        public string[] CritPhrases => critPhrases;

        #endregion
    }
}
