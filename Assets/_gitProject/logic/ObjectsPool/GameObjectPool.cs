using UnityEngine;

namespace _gitProject.logic.ObjectsPool {
    public class GameObjectPool : PoolBase<GameObject> {
        
        #region constructor
        public GameObjectPool(GameObject prefab, string containerName,Transform container, int preloadCount)
            : base(() => Preload(prefab, containerName, container), 
                GetAction, ReturnAction, preloadCount, container) {
        }
        #endregion

        #region private methods

        private static GameObject Preload(GameObject prefab, string containerName, Transform containerTransform) {
            var o = Object.Instantiate(prefab);
            o.transform.parent = containerTransform;
            containerTransform.name = containerName;
            return o;
        }
        private static void GetAction(GameObject @object) => @object.SetActive(true);
        private static void ReturnAction(GameObject @object) => @object.SetActive(false);

        #endregion
    }
}
