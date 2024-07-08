using System;
using _gitProject.logic.Services;
using _gitProject.logic.ViewCamera;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _gitProject.logic.Components {
    public class PopUpVisualReaction : VisualReaction {
        private float _value;
        public PopUpVisualReaction(Transform parent) : base(parent) {}
        public override void React<T>(GameObject prefab, T value) {
            var popUp = Object.Instantiate(prefab,Parent.position + Vector3.up * 3,Parent.rotation,Parent);
            popUp.transform.LookAt(ServiceLocator.Current.Get<CameraBehaviour>().transform.position);
            var text = popUp.GetComponentInChildren<TextMeshPro>();
            var textValue = value.ToString();
            if (value is int) {
                var intValue = Convert.ToInt32(value);
                text.text = textValue;
                if (intValue > 1) text.color = Color.yellow;
                text.fontSize = 5 + intValue * 0.2f;
                popUp.transform.DOShakePosition(0.15f, 2f);
                popUp.transform.DOPunchRotation(Vector3.down * 2, 0.15f, 2,1f);
                Object.Destroy(popUp,0.85f);
            }
            if (value is string) {
                text.text = textValue;
                text.fontSize = 5;
                text.color = Color.white;
                popUp.transform.DOShakePosition(0.15f, 10f);
                Object.Destroy(popUp,0.85f);
            }
        }
    }
}
