using _gitProject.logic.Components;
using _gitProject.logic.Interfaces;
using UnityEngine;

namespace _gitProject.logic.Enemies {
    [RequireComponent(typeof(AudioSource))]
    public class Enemy : MonoBehaviour, IDamageable {

        private Health _health;
        private ColorChanger _colorChanger;
        private HitSoundReaction _hitSoundReaction;

        [SerializeField] private AudioClip[] _hitClips;
        
        
        private void Awake() {
            _health = new Health(10);
            _colorChanger = new ColorChanger(_health.GetHealth, GetComponent<Renderer>());
            _hitSoundReaction = new HitSoundReaction(_hitClips, GetComponent<AudioSource>());
        }
        
        private void OnEnable() {
            _health.OnDied += DestroySelf;
            _health.OnHealthChanged += _colorChanger.ChangeGradientColor;
            _health.OnHealthTriggered += _hitSoundReaction.PlayReactionSound;
        }
        private void OnDisable() {
            _health.OnDied -= DestroySelf;
            _health.OnHealthChanged -= _colorChanger.ChangeGradientColor;
            _health.OnHealthTriggered -= _hitSoundReaction.PlayReactionSound;
        }
        public void TakeDamage(int amount) => _health.Reduce(amount);
        private void DestroySelf() => Destroy(gameObject);
    }
}
