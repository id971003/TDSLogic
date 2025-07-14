using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;

public class Truck : MonoBehaviour
{
    GameManager _gameManager;
    Coroutine rootCorutine;

    public void Start()
    {
        _gameManager= GameManager.Instance; 
    }
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
            if (_gameManager.B_Move)
            {
                transform.Translate(Data.TruckMoveVector);
            }
        }
    }
}
