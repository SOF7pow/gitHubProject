using System;
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
    }
}
