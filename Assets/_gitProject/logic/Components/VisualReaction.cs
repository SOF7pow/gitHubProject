using _gitProject.logic.Components.Labels;
using TMPro;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;
using _gitProject.logic.Services;
using _gitProject.logic.ViewCamera;

namespace _gitProject.logic.Components {
    public class VisualReaction {
        private readonly Transform _parent;
        public VisualReaction(Transform parent) => _parent = parent;
        public void React(GameObject prefab, float value) 
        {
            var effect = CreateVisualEffect(prefab, 4f);
            //popUpDamage
            if (effect.TryGetComponent<PopUpLabel>(out var popUp)) 
            {
                effect.transform.LookAt(ServiceLocator.Current.Get<CameraBehaviour>().transform.position);
                var popUpText = popUp.GetComponentInChildren<TextMeshPro>();
                popUpText.text = value.ToString();
                popUpText.fontSize = 10 + value * 0.2f;
                
                if (!(value > 5)) return;
                popUpText.color = Color.yellow;
                effect.transform.DOShakePosition(0.15f, 5f);
            }
            //simple effect
            else effect.transform.localScale *= value;
        }
        
        public void React(GameObject prefab, string text) {
            var popUp = CreateVisualEffect(prefab, 5f);
            popUp.transform.DOShakePosition(0.15f, 5f);
            popUp.transform.LookAt(ServiceLocator.Current.Get<CameraBehaviour>().transform.position);
            var popUpText = popUp.GetComponentInChildren<TextMeshPro>();
            popUpText.text = text;
            popUpText.fontSize = 5;
            popUpText.color = Color.white;
        }
        private GameObject CreateVisualEffect(GameObject prefab ,float vectorY) {
            var obj = Object.Instantiate(prefab,_parent.position + Vector3.up * vectorY, _parent.rotation, _parent);
            Object.Destroy(obj,0.7f);
            return obj;
        }
    }
}
