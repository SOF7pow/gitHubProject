using _gitProject.logic.Services;
using _gitProject.logic.ViewCamera;
using TMPro;
using UnityEngine;

namespace _gitProject.logic.Components {
    public class PopUpReaction {
        private readonly GameObject _popUp;
        private Transform _parent;
        public PopUpReaction(GameObject popUp, Transform parent) {
            _popUp = popUp;
            _parent = parent;
        }
        public void CreatePopUp(int value) {
            var popUp = Object.Instantiate(_popUp,_parent.position + Vector3.up * 3,_parent.rotation,_parent);
            popUp.transform.LookAt(ServiceLocator.Current.Get<CameraFollow>().transform.position);
            var text = popUp.GetComponentInChildren<TextMeshPro>();
            text.text = value.ToString();
            text.fontSize = value * 2;
            Object.Destroy(popUp,1f);
        }
    }
}
