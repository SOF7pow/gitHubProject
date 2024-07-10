using System.Collections;
using _gitProject.logic.Components.Labels;
using _gitProject.logic.ObjectsPool;
using _gitProject.logic.Services;
using _gitProject.logic.ViewCamera;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _gitProject.logic.Components {
    public class VisualReaction {
        
        private readonly Transform _parent;
        private MonoBehaviour _monoContext;
        public VisualReaction(MonoBehaviour monoContext, Transform parent) {
            _monoContext = monoContext;
            _parent = parent;
        }
        public void EffectReact(GameObject prefab, float value) {
            var effect = CreateVisualEffect(prefab, 0.5f);
            effect.transform.localScale *= value * 0.5f;
        }
        public void PopUpReact(GameObjectPool pool, string text, float value) {
            var effect = pool.Get();
            _monoContext.StartCoroutine(SetDelayToReturn(effect, pool, 0.25f));
            effect.transform.SetPositionAndRotation(_parent.transform.position + Vector3.up * 2f, _parent.transform.rotation);
            effect.transform.DOShakePosition(0.15f, 5f);
            effect.transform.LookAt(ServiceLocator.Current.Get<CameraBehaviour>().transform.position);
            var popUpText = effect.GetComponentInChildren<TextMeshPro>();
            popUpText.text = text;
            popUpText.fontSize = 10 + value * 0.5f;
            if (value > 1f) {
                popUpText.color = Color.yellow;
                effect.transform.DOShakePosition(0.15f, 10f);
            }
            else
                popUpText.color = Color.white;
        }
        private GameObject CreateVisualEffect(GameObject prefab, float vectorY) {
            var obj = Object.Instantiate(prefab, _parent.position + Vector3.up * vectorY, _parent.rotation, _parent);
            Object.Destroy(obj, 0.7f);
            return obj;
        }
        public IEnumerator SetDelayToReturn(GameObject prefab, GameObjectPool pool, float delay) {
            yield return new WaitForSeconds(delay);
            pool.Return(prefab);
        }
    }
}
