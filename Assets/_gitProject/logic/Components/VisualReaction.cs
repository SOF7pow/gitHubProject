using UnityEngine;

namespace _gitProject.logic.Components {
    public abstract class VisualReaction {
        protected readonly Transform Parent;
        protected VisualReaction(Transform parent) {
            Parent = parent;
        }
        public abstract void React<T>(GameObject prefab, T value);
    }
}
