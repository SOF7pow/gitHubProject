using System;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _gitProject.logic.Components {
    public class EffectVisualReaction : VisualReaction {

        public EffectVisualReaction(Transform parent) : base(parent){}
        
        public override void React<T>(T value, GameObject prefab) {
            var effect = Object.Instantiate(prefab,Parent.position,Parent.rotation,Parent);
            var intValue = Convert.ToInt32(value);
            effect.transform.localScale *= intValue;
            effect.transform.DOPunchScale(Vector3.one * intValue, 0.2f,1,0.5f);
            effect.transform.DOPunchRotation(Vector3.right * (intValue * 5), 0.2f,1,1f);
            Object.Destroy(effect,1f);
        }
    }
}
