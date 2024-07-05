using UnityEngine;

namespace _gitProject.logic.Components {
    public class ColorChanger {
        
        private Gradient _colorGradient;
        private readonly int _maxValue;
        private readonly Renderer _renderer;
        private readonly Color _first = Color.black;
        private readonly Color _second = Color.red;
        
        public ColorChanger(int healthValue, Renderer meshRenderer) {
            _renderer = meshRenderer;
            _maxValue = healthValue;
            SetStartColorGradient();
        }
        
        private void SetStartColorGradient() {
            _colorGradient = new();
            var colors = new GradientColorKey[2];
            var alphas = new GradientAlphaKey[2];
            colors[0] = new GradientColorKey(_first, 1.0f);
            colors[1] = new GradientColorKey(_second, 0.0f);
            _colorGradient.SetKeys(colors,alphas);
            _renderer.material.color = _first;
        }
        
        public void ChangeGradientColor(int value) {
            var deltaHealth = (float) value / _maxValue;
            _renderer.material.color = _colorGradient.Evaluate(deltaHealth);
        }
    }
}
