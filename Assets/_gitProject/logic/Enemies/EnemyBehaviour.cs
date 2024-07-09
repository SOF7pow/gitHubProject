using _gitProject.logic.Components;
using _gitProject.logic.Events;
using _gitProject.logic.Interfaces;
using _gitProject.logic.Services;
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
        public void Initialize() {
            _health = new Health(_healthValue);
            _healthColorChanger = new HealthColorChanger(_health.GetHealth, GetComponent<Renderer>(), Color.black, Color.red);
            _soundReaction = new SoundReaction(GetComponent<AudioSource>());
            _positionFollower = new PositionFollower(GetComponent<NavMeshAgent>());
            _visualReaction = new VisualReaction(transform);
            _health.OnHealthChanged += _healthColorChanger.ChangeGradientColor;
            _health.OnDied += () => _soundReaction.React(ServiceLocator.Current.Get<SoundsData>().Storage.DieSounds,0.75f);
            _health.OnDied += DestroySelf;
            EventBus.Instance.OnUpdatePlayerPositionData += _positionFollower.UpdateTargetPosition;
        }
        private void OnDisable() {
            _health.OnHealthChanged -= _healthColorChanger.ChangeGradientColor;
            _health.OnDied -= () => _soundReaction.React(ServiceLocator.Current.Get<SoundsData>().Storage.DieSounds,1f);
            _health.OnDied -= DestroySelf;
            EventBus.Instance.OnUpdatePlayerPositionData -= _positionFollower.UpdateTargetPosition;
        }
        public void TakeDamage(int amount) {
            transform.Translate(Vector3.forward * (-amount * 0.25f));
            transform.DOPunchScale(Vector3.one * (amount * 0.2f), 0.15f, 2);
            transform.DOMoveY(transform.position.y * 1.5f, 0.1f);
            _soundReaction.React(ServiceLocator.Current.Get<SoundsData>().Storage.HitSounds, 0.25f);
            _visualReaction.React(ServiceLocator.Current.Get<PrefabsData>().Storage.PopUpDamage, amount);
            _visualReaction.React(ServiceLocator.Current.Get<PrefabsData>().Storage.BaseHitEffect, amount * 0.5f);
            _health.Reduce(amount);
        }
        private void DestroySelf() {
            GetComponent<Collider>().enabled = false;
            GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(gameObject, 0.25f);
        }
    }
}
