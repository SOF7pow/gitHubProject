using _gitProject.logic.Components;
using _gitProject.logic.Events;
using _gitProject.logic.Interfaces;
using _gitProject.logic.ObjectsPool;
using _gitProject.logic.Storage;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace _gitProject.logic.Enemies {
    [RequireComponent(typeof(NavMeshAgent),typeof(AudioSource))]
    public class EnemyBehaviour : MonoBehaviour, IDamageable {
        
        [SerializeField] private int _healthValue = 20;
        
        private Health _health;
        private HealthColorChanger _healthColorChanger;
        private SoundReaction _soundReaction;
        private PositionFollower _positionFollower;
        private VisualReaction _visualReaction;
        private GameObjectPool _popUpPool;
        private SoundsStorage _soundsStorage;
        private PrefabsStorage _prefabsStorage;
        
        public void Initialize(GameObjectPool popUpPool, SoundsStorage soundsStorage, PrefabsStorage prefabsStorage) {
            _popUpPool = popUpPool;
            _soundsStorage = soundsStorage;
            _prefabsStorage = prefabsStorage;
            _health = new Health(_healthValue);
            _healthColorChanger = new HealthColorChanger(_health.GetHealth, GetComponent<Renderer>(), Color.black, Color.red);
            _soundReaction = new SoundReaction(GetComponent<AudioSource>());
            _positionFollower = new PositionFollower(GetComponent<NavMeshAgent>());
            _visualReaction = new VisualReaction(this, transform);
            _health.OnHealthChanged += _healthColorChanger.ChangeGradientColor;
            _health.OnDied += () => _soundReaction.React(_soundsStorage.Storage.DieSounds,0.75f);
            _health.OnDied += DestroySelf;
            EventsStorage.Instance.OnUpdatePlayerPositionData += _positionFollower.UpdateTargetPosition;
        }
        private void OnDisable() {
            _health.OnHealthChanged -= _healthColorChanger.ChangeGradientColor;
            _health.OnDied -= () => _soundReaction.React(_soundsStorage.Storage.DieSounds,1f);
            _health.OnDied -= DestroySelf;
            EventsStorage.Instance.OnUpdatePlayerPositionData -= _positionFollower.UpdateTargetPosition;
        }
        public void TakeDamage(int amount) {
            transform.Translate(Vector3.forward * (-amount * 0.025f));
            transform.DOShakePosition(0.15f, amount*0.5f, 2);
            _soundReaction.React(_soundsStorage.Storage.HitSounds, 0.25f);
            _visualReaction.EffectReact(_prefabsStorage.Storage.BaseHitEffect, amount);
            _visualReaction.PopUpReact(_popUpPool, amount.ToString(), amount);
            
            _health.Reduce(amount);
        }
        private void DestroySelf() {
            GetComponent<Collider>().enabled = false;
            GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(gameObject, 0.3f);
        }
    }
}
