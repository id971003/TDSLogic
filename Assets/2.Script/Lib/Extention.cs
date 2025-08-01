using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public static class Extention
    {
        public static void MoveToEnd<T>(this List<T> list, int index)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (index < 0 || index >= list.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            T item = list[index];
            list.RemoveAt(index);
            list.Add(item);
        }
        public static bool RemoveBySwap<T>(this List<T> list,T target)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            
            int index = list.IndexOf(target);
            if (index >= 0)
            {
                list[index] = list[list.Count - 1]; // Swap with the last element
                list.RemoveAt(list.Count - 1); // Remove the last element
                return true;
            }
            return false;
        }
    }
    public static class ComponentExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            var comp = go.GetComponent<T>();
            if (comp != null)
                return comp;

            return go.AddComponent<T>();
        }
        public static T GetComponentNoGarbage<T>(this Component c)
        {
            if (c.TryGetComponent(out T ret))
            {
                return ret;
            }
            return default(T);
        }

        public static T GetComponentNoGarbage<T>(this GameObject c)
        {
            if (c.TryGetComponent(out T ret))
            {
                return ret;
            }
            return default(T);
        }
    }
}