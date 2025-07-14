using System.Collections.Generic;
using UnityEngine;

public static class Yielders
{
    private static Dictionary<float, WaitForSeconds> waitForSecondseCaches = new Dictionary<float, WaitForSeconds>(new FloatComparer());
    private static WaitForEndOfFrame waitForEndOfFrameCache = new WaitForEndOfFrame();
    private static WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private static Dictionary<float, WaitForSecondsRealtime> waitForSecondsRealTimeCaches = new Dictionary<float, WaitForSecondsRealtime>();


    class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }
        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static WaitForSeconds WaitForSeconds(float duration)
    {
        if (!waitForSecondseCaches.TryGetValue(duration, out var wfs))
            waitForSecondseCaches.Add(duration, wfs = new WaitForSeconds(duration));
        return wfs;
    }

    public static WaitForEndOfFrame WaitForEndOfFrame() => waitForEndOfFrameCache;

    public static WaitForFixedUpdate WaitForFixedUpdate() => waitForFixedUpdate;

    public static WaitForSecondsRealtime WaitForSecondsRealtime(float duration)
    {
        if (!waitForSecondsRealTimeCaches.TryGetValue(duration, out var wfsr))
            waitForSecondsRealTimeCaches.Add(duration, wfsr = new WaitForSecondsRealtime(duration));
        return wfsr;
    }
}