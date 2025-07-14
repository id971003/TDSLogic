using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;

public class Monster : Poolable
{
    [SerializeField] private GameObject view;
    [SerializeField] private int layer = 0;


    Coroutine rootMove;

    public void SetUp(int Layer)
    {
        layer = Layer;
        gameObject.layer = LayerMask.NameToLayer(Data.LayerName[Layer]);
        



        if(rootMove != null)
            StopCoroutine(rootMove);
        rootMove = StartCoroutine(MoveRoot());

    }

    IEnumerator MoveRoot()
    {
        while (true)
        {
            view.transform.position += new Vector3(-Data.EnemySpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }

}
