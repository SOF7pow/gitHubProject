using UnityEngine;

namespace _gitProject.logic.Components {
    public class ColorChanger {

        #region fields

        private Gradient _colorGradient;
        private readonly int _maxValue;
        private readonly Renderer _renderer;
        private readonly Color _first;
        private readonly Color _second;

        #endregion

        #region constructor

        public ColorChanger(int healthValue, Renderer meshRenderer, Color first, Color second) {
            _renderer = meshRenderer;
            _maxValue = healthValue;
            _first = first;
            _second = second;
            
            SetStartColorGradient();
        }

        #endregion

        #region public methods

        public void ChangeGradientColor(int value) {
            var deltaHealth = (float) value / _maxValue;
            _renderer.material.color = _colorGradient.Evaluate(deltaHealth);
        }

        #endregion

        #region private methods

        private void SetStartColorGradient() {
            _colorGradient = new();
            var colors = new GradientColorKey[2];
            var alphas = new GradientAlphaKey[2];
            colors[0] = new GradientColorKey(_first, 1.0f);
            colors[1] = new GradientColorKey(_second, 0.0f);
            _colorGradient.SetKeys(colors, alphas);
            _renderer.material.color = _first;
        }

        #endregion
    }
}
