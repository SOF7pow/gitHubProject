using _gitProject.logic.Components;
using _gitProject.logic.Events;
using _gitProject.logic.Interfaces;
using _gitProject.logic.ObjectsPool;
using _gitProject.logic.Storage;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace _gitProject.logic.EnemyLogic {
    [RequireComponent(typeof(NavMeshAgent),typeof(AudioSource))]
    public class EnemyBehaviour : MonoBehaviour, IDamageable{

        #region fields

        [SerializeField] private int healthValue = 20;
        
        private Health _health;
        private ColorChanger _colorChanger;
        private SoundReaction _soundReaction;
        private PositionFollower _positionFollower;
        private VisualReaction _visualReaction;
        private GameObjectPool _popUpPool;
        private GameObjectPool _hitPool;
        private SoundsStorage _soundsStorage;
        private NavMeshAgent _navMeshAgent;
        
        #endregion

        #region initialization

        public void Init(
            GameObjectPool popUpPool, 
            GameObjectPool hitPool, 
            SoundsStorage soundsStorage) 
        {
            _popUpPool = popUpPool;
            _hitPool = hitPool;
            _soundsStorage = soundsStorage;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.isStopped = true;
            
            _health = new Health(healthValue);
            _colorChanger = new ColorChanger(_health.GetHealth, GetComponent<Renderer>(), Color.black ,Color.red);
            _soundReaction = new SoundReaction(GetComponent<AudioSource>());
            _positionFollower = new PositionFollower(_navMeshAgent);
            _visualReaction = new VisualReaction(this, transform);
            
            Enable();
        }

        #endregion

        #region unity callbacks
        private void OnDisable() => Dispose();

        #endregion

        #region public methods

        public void TakeDamage(int amount) {
            transform.Translate(Vector3.forward * (-amount * 0.025f));
            transform.DOShakePosition(0.15f, amount*0.5f, 2);
            _soundReaction.React(_soundsStorage.Storage.HitSounds, 0.25f);
            _visualReaction.EffectReact(_hitPool, amount);
            _visualReaction.PopUpReact(_popUpPool, amount.ToString(), amount);
            _health.Reduce(amount);
        }
        
        public void OnStartGame() => _navMeshAgent.isStopped = false;
        public void OnPauseGame() => _navMeshAgent.isStopped = true;
        public void OnResumeGame() => _navMeshAgent.isStopped = false;
        public void OnFinishGame() => _navMeshAgent.isStopped = true;

        #endregion

        #region private methods
        private void  Enable() {
            _health.OnHealthChanged += _colorChanger.ChangeGradientColor;
            _health.OnDied += () => _soundReaction.React(_soundsStorage.Storage.DieSounds,0.75f);
            _health.OnDied += DestroySelf;
            
            EventBus.Instance.OnUpdatePlayerPositionData += _positionFollower.UpdateTargetPosition;
            EventBus.Instance.OnGameStart += OnStartGame;
            EventBus.Instance.OnGamePause += OnPauseGame;
            EventBus.Instance.OnGameResume += OnResumeGame;
            EventBus.Instance.OnGameFinish += OnFinishGame;
        }

        private void Dispose() {
            _health.OnHealthChanged -= _colorChanger.ChangeGradientColor;
            _health.OnDied -= () => _soundReaction.React(_soundsStorage.Storage.DieSounds,1f);
            _health.OnDied -= DestroySelf;
            
            EventBus.Instance.OnUpdatePlayerPositionData -= _positionFollower.UpdateTargetPosition;
            EventBus.Instance.OnGameFinish -= OnStartGame;
            EventBus.Instance.OnGamePause -= OnPauseGame;
            EventBus.Instance.OnGameResume -= OnResumeGame;
            EventBus.Instance.OnGameFinish -= OnFinishGame;
        }
        
        private void DestroySelf() {
            GetComponent<Collider>().enabled = false;
            GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(gameObject, 0.3f);
        }

        #endregion
    }
}
