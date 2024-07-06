using _gitProject.logic.Components;
using _gitProject.logic.Events;
using _gitProject.logic.Interfaces;
using _gitProject.logic.Services;
using UnityEngine;
using UnityEngine.AI;

namespace _gitProject.logic.Enemies {
    [RequireComponent(typeof(NavMeshAgent),typeof(AudioSource))]
    public class EnemyController : MonoBehaviour, IDamageable {
        
        private Health _health;
        private ColorChanger _colorChanger;
        private SoundReaction _hitReaction;
        private SoundReaction _dieReaction;
        private TargetChaser _targetChaser;
        private PopUpReaction _popUpReaction;

        public void Initialize() {
            var source = GetComponent<AudioSource>();
            var soundStorage = ServiceLocator.Current.Get<SoundStorage>();
            var prefabStorage = ServiceLocator.Current.Get<PrefabStorage>();
            
            _health = new Health(10);
            _colorChanger = new ColorChanger(_health.GetHealth, GetComponent<Renderer>());
            _hitReaction = new SoundReaction(soundStorage.HitClips, source);
            _dieReaction= new SoundReaction(soundStorage.DieClips, source);
            _targetChaser = new TargetChaser(GetComponent<NavMeshAgent>());
            _popUpReaction = new PopUpReaction(prefabStorage.PopUpDamage, transform);
            
            _health.OnHealthChanged += _colorChanger.ChangeGradientColor;
            _health.OnHealthTriggered += _hitReaction.React;
            _health.OnDied += _dieReaction.React;
            _health.OnDied += DestroySelf;
            
            EventBus.Instance.OnUpdatePlayerPosition += _targetChaser.UpdateTargetPosition;
        }
        private void OnDisable() {
            _health.OnHealthChanged -= _colorChanger.ChangeGradientColor;
            _health.OnHealthTriggered -= _hitReaction.React;
            _health.OnDied -= _dieReaction.React;
            _health.OnDied -= DestroySelf;
            EventBus.Instance.OnUpdatePlayerPosition -= _targetChaser.UpdateTargetPosition;
        }
        public void TakeDamage(int amount) {
            _health.Reduce(amount);
            _popUpReaction.CreatePopUp(amount);
        }

        private void DestroySelf() => Destroy(gameObject);
    }
}
