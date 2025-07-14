using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public static class Extention
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            var comp = go.GetComponent<T>();
            if (comp != null)
                return comp;

            return go.AddComponent<T>();
        }
    }
}