using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _gitProject.logic.ExtensionMethods {
    public static class Extensions {
        public static T GetRandom<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Array is null or empty", nameof(array));
            var index = Random.Range(0,array.Length);
            return array[index];
        }
        
        public static Color GetRandomColor() {
            var randomColor = Random.Range(0, 3);
            switch (randomColor) {
                case 0:
                    return new Color(0.89f, 0.44f, 0.48f,1f);
                case 1:
                    return new Color(0.47f, 0.87f, 0.47f, 1);
                case 2:
                    return new Color(0.5f, 0.78f, 1f, 1f);
                default:
                    return new Color(1f, 1f, 1f,1f);
            }
        }
    }
}
