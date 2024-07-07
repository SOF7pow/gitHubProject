using _gitProject.logic.Components;
using _gitProject.logic.Events;
using _gitProject.logic.Interfaces;
using _gitProject.logic.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace _gitProject.logic.Enemies {
    [RequireComponent(typeof(NavMeshAgent),typeof(AudioSource))]
    public class EnemyController : MonoBehaviour, IDamageable {
        
        private Health _health;
        private ColorChanger _colorChanger;
        private SoundReaction _soundReaction;
        private TargetChaser _targetChaser;
        private PopUpVisualReaction _popUpVisualReaction;
        private EffectVisualReaction _hitVisualReaction;
        private Collider _collider;
        private NavMeshAgent _agent;
        private SoundsData _soundsData;
        private PrefabsData _prefabsData;

        [SerializeField] private int _healthValue = 20;
        public void Initialize() {
            var source = GetComponent<AudioSource>();
            _prefabsData = ServiceLocator.Current.Get<PrefabsData>();
            
            _collider ??= GetComponent<Collider>();
            _agent ??= GetComponent<NavMeshAgent>();
            _soundsData = ServiceLocator.Current.Get<SoundsData>();
            

            _health = new Health(_healthValue);
            _colorChanger = new ColorChanger(_health.GetHealth, GetComponent<Renderer>(), Color.black, Color.red);
            _soundReaction = new SoundReaction(source);
            
            _targetChaser = new TargetChaser(_agent);
            _popUpVisualReaction = new PopUpVisualReaction(_prefabsData.Storage.PopUpDamage, transform);
            _hitVisualReaction = new EffectVisualReaction(transform);
            
            _health.OnHealthChanged += _colorChanger.ChangeGradientColor;
            _health.OnDied += () => _soundReaction.React(_soundsData.Storage.Die,1f);
            _health.OnDied += DestroySelf;
            EventBus.Instance.OnUpdatePlayerPosition += _targetChaser.UpdateTargetPosition;
        }
        private void OnDisable() {
            _health.OnHealthChanged -= _colorChanger.ChangeGradientColor;
            _health.OnDied -= () => _soundReaction.React(_soundsData.Storage.Die,1f);
            _health.OnDied -= DestroySelf;
            EventBus.Instance.OnUpdatePlayerPosition -= _targetChaser.UpdateTargetPosition;
        }
        public void TakeDamage(int amount) {
            transform.Translate(Vector3.forward * (-amount * 0.25f));
            transform.DOPunchScale(Vector3.one * (amount * 0.2f), 0.15f, 2);
            transform.DOMoveY(transform.position.y * 1.5f, 0.1f);
            
            _soundReaction.React(_soundsData.Storage.Hit, 0.25f);
            _popUpVisualReaction.React(amount, _prefabsData.Storage.PopUpDamage);
            _hitVisualReaction.React(amount, _prefabsData.Storage.BaseHitEffect);
            
            _health.Reduce(amount);
        }
        
        private void DestroySelf() {
            _collider.enabled = false;
            _agent.isStopped = true;
            Destroy(gameObject, 0.25f);
        }
    }
}
