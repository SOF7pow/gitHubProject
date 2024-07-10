using UnityEngine;

namespace _gitProject.logic.ObjectsPool {
    public class GameObjectPool : PoolBase<GameObject> {
        #region constructor
        public GameObjectPool(GameObject prefab, string parentName, int preloadCount, Transform container)
            : base(() => Preload(prefab, parentName, container), GetAction, ReturnAction, preloadCount, container) {
        }
        #endregion

        #region public methods

        public static GameObject Preload(GameObject prefab, string containerName, Transform container) {
            var o = Object.Instantiate(prefab);
            o.transform.parent = container;
            container.name = containerName;
            return o;
        }

        public static void GetAction(GameObject @object) => @object.SetActive(true);

        public static void ReturnAction(GameObject @object) => @object.SetActive(false);

        #endregion
    }
}
