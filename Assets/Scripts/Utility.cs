// <copyright file="Extensions.cs" >
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Adam Streck</author>
// <date> 08/3/2015 </date>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Neomento
{
    public static class Extensions
    {
        /// <summary>
        /// Applies the action to each element of the container in place.
        /// </summary>
        /// <typeparam name="T">The type of elemtens in the container.</typeparam>
        /// <param name="container">A container whose elements are to be affected.</param>
        /// <param name="action">The function to be applied to the elements in the container.</param>
        public static void ForEach<T>(this IEnumerable<T> container, Action<T> action)
        {
            foreach (T element in container)
            {
                action(element);
            }
        }


        public static void ForEach<T>(this IEnumerable<T> container, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in container) action(e, i++);
        }


        public static IEnumerable<T> SkipIndex<T>(this IEnumerable<T> source, int index)
        {
            int counter = 0;
            foreach (var item in source)
            {
                if (counter != index)
                    yield return item;

                counter++;
            }
        }

        /// <summary>
        /// Returns a random element from an enumerable object (array also counts).
        /// </summary>
        /// <typeparam name="T">The enumerable object type.</typeparam>
        /// <param name="enumerable">The enumerable object to return an element from.</param>
        /// <returns>A random element from the enumerable object.</returns>
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            var index = UnityEngine.Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }

        /// <summary>
        /// Returns all monobehaviours (casted to T)
        /// </summary>
        /// <typeparam name="T">interface type</typeparam>
        /// <param name="gObj"></param>
        /// <returns></returns>
        public static T[] GetInterfaces<T>(this GameObject gObj)
        {
            if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
            var mObjs = gObj.GetComponents<MonoBehaviour>();

            return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T)) select (T) (object) a)
                .ToArray();
        }

        /// <summary>
        /// Returns the first monobehaviour that is of the interface type (casted to T)
        /// </summary>
        /// <typeparam name="T">Interface type</typeparam>
        /// <param name="gObj"></param>
        /// <returns></returns>
        public static T GetInterface<T>(this GameObject gObj)
        {
            if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
            return gObj.GetInterfaces<T>().FirstOrDefault();
        }

        /// <summary>
        /// Returns the first instance of the monobehaviour that is of the interface type T (casted to T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gObj"></param>
        /// <returns></returns>
        public static T GetInterfaceInChildren<T>(this GameObject gObj)
        {
            if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
            return gObj.GetInterfacesInChildren<T>().FirstOrDefault();
        }

        /// <summary>
        /// Gets all monobehaviours in children that implement the interface of type T (casted to T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gObj"></param>
        /// <returns></returns>
        public static T[] GetInterfacesInChildren<T>(this GameObject gObj)
        {
            if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");

            var mObjs = gObj.GetComponentsInChildren<MonoBehaviour>();

            return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T)) select (T) (object) a)
                .ToArray();
        }


        public static int LayerToMask(this int layerNo)
        {
            return 1 << layerNo;
        }


        public static bool IsRight(this GameObject gameObject)
        {
            if (gameObject.tag == "Right")
            {
                return true;
            }

            if (gameObject.tag != "Left")
            {
                Debug.LogError("IsRight called on object without a Left or Right tag");
            }

            return false;
        }

        public static void Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static void Resize<T>(this List<T> list, int sz, T c = default(T))
        {
            int cur = list.Count;
            if (sz < cur)
                list.RemoveRange(sz, cur - sz);
            else if (sz > cur)
                list.AddRange(Enumerable.Repeat(c, sz - cur));
        }

        internal static int LayerMaskToLayer(this LayerMask mask)
        {
            int layer_no = 0;
            int value = mask.value;
            if (value == 0)
            {
                Debug.LogError("LayerMask is empty in LayerMaskToLayer");
            }

            for (int i = 0; i < 32; i++)
            {
                if (value % 2 == 1)
                {
                    if (value != 1)
                    {
                        Debug.LogError("LayerMask has more than layer in LayerMaskToLayer");
                    }

                    return layer_no;
                }
                else
                {
                    value /= 2;
                    layer_no++;
                }
            }

            return -1;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new System.Random());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, System.Random rng)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (rng == null) throw new ArgumentNullException("rng");

            return source.ShuffleIterator(rng);
        }

        private static IEnumerable<T> ShuffleIterator<T>(
            this IEnumerable<T> source, System.Random rng)
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        public static string ToSentence(this string Input)
        {
            return new string(Input.SelectMany((c, i) => i > 0 && char.IsUpper(c) ? new[] {' ', c} : new[] {c})
                .ToArray());
        }


        public static T[,] Transpose<T>(this T[,] array2D)
        {
            T[,] result = new T[array2D.GetLength(1) - 1, array2D.GetLength(0) - 1];

            for (int i = 0; i < array2D.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < array2D.GetLength(1) - 1; j++)
                {
                    result[j, i] = array2D[i, j];
                }
            }

            return result;
        }
    }
}