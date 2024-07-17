using System.Collections;
using _gitProject.logic.ObjectsPool;
using _gitProject.logic.Services;
using _gitProject.logic.ViewCamera;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _gitProject.logic.Components {
    public class VisualReaction {

        #region fields

        private readonly Transform _parent;
        private readonly MonoBehaviour _monoContext;

        #endregion

        #region constructor

        public VisualReaction(MonoBehaviour monoContext, Transform parent) {
            _monoContext = monoContext;
            _parent = parent;
        }

        #endregion

        #region public methods

        public void EffectReact(GameObjectPool pool, float value) {
            var effect = pool.Get();
            effect.transform.SetPositionAndRotation(_parent.transform.position + Vector3.up * 2f, _parent.transform.rotation);
            _monoContext.StartCoroutine(SetDelayToReturn(effect, pool, 0.25f));
            effect.transform.localScale = Vector3.one * value;
            effect.transform.DOShakePosition(0.15f, value, 10);
        }   
        public void EffectReact(GameObject prefab, float value) {
            var effect = Object.Instantiate(prefab, _parent);
            effect.transform.localScale = Vector3.one * value;
            effect.transform.SetPositionAndRotation(_parent.transform.position + Vector3.up * 2f, _parent.transform.rotation);
            effect.transform.DOShakePosition(0.15f, value, 10);
            Object.Destroy(effect,0.5f);
        }
        
        public void PopUpReact(GameObjectPool pool, string text, float value) {
            var effect = pool.Get();
            _monoContext.StartCoroutine(SetDelayToReturn(effect, pool, 0.25f));
            effect.transform.SetPositionAndRotation(_parent.transform.position + Vector3.up * 3f, _parent.transform.rotation);
            effect.transform.DOShakePosition(0.15f, 5f);
            effect.transform.LookAt(ServiceLocator.Current.Get<CameraBehaviour>().transform.position);
            var popUpText = effect.GetComponentInChildren<TextMeshPro>();
            popUpText.text = text;
            popUpText.fontSize = 5 + value * 0.5f;
            if (value > 1f) {
                popUpText.color = Color.yellow;
                effect.transform.DOShakePosition(0.15f, 5f,5,45f);
            }
            else
                popUpText.color = Color.white;
        }

        #endregion

        #region private methods

        private IEnumerator SetDelayToReturn(GameObject prefab, GameObjectPool pool, float delay) {
            yield return new WaitForSeconds(delay);
            pool.Return(prefab);
        }

        #endregion
    }
}
