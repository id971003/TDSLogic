using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;

public class Truck : MonoBehaviour
{
    Coroutine rootCorutine;
    public void RootStart()
    {
        if(rootCorutine != null)
            StopCoroutine(rootCorutine);
        rootCorutine= StartCoroutine(Root());
    }
    public IEnumerator Root()
    {
        while(true)
        {
            yield return null;
            transform.Translate(Data.TruckMoveVector);
        }
    }
}
