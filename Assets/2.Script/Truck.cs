using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;

public class Truck : MonoBehaviour
{
    [SerializeField] private Transform trans_FrontWhell;
    [SerializeField] private Transform trans_BackWhell;

    
    [SerializeField] private Hero Hero;
     public List<HitObject> boxes = new List<HitObject>();


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
        while (true)
        {
            yield return null;
            if (_gameManager.B_Move)
            {
                transform.Translate(Data.TruckMoveVector);
                
                trans_FrontWhell.Rotate(Vector3.forward, Data.WheelRotateSpeed);
                trans_BackWhell.Rotate(Vector3.forward, Data.WheelRotateSpeed);
            }
        }
    }





    //적이랑 가장 가까운 박스 반환
    public Box ReturnClosedBox(Transform trans)
    {
        Box closestBox = null;
        float closestDistance = float.MaxValue;
        foreach (Box box in boxes)
        {
            if (!box.B_Alive) continue; // Skip dead boxes
            float distance = Vector3.Distance(trans.position, box.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestBox = box;
            }
        }
        if(closestBox == null) 
            return null; // No boxes available
        return closestBox;  
    }

    //박스 터젔을때 재 정렬
    public void SetBoxInfo()
    {
        
        for(int i=0;i< boxes.Count;i++)
        {
            boxes[i].transform.localPosition =  Data.BoxStartPositionY + (Data.BoxGapY * i);
        }
        if (boxes.Count == 0)
        {
            Hero.transform.localPosition = Data.BoxStartPositionY;
        }
        else
        { 
            Hero.transform.localPosition = Data.BoxStartPositionY + (Data.BoxGapY * (boxes.Count - 1)) + (Data.HeroGapY);
        }

    }
}
