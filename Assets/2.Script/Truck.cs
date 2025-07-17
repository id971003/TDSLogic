using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;
using TaskbarHero;

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
        SetUp();

    }
    void SetUp()
    {
        foreach(var a in boxes)
        {
            var box = a as Box;
            box.SetUp(this);
        }
        Hero.SetUp(this);
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
    public HitObject ReturnCurrentHitPoj(Transform trans)
    {
        Box closestBox = null;
        float closestDistance = float.MaxValue;
        foreach (Box box in boxes)
        {
            if (!box.B_Alive) continue; // 죽은 박스는 스킵
            float distance = Vector3.Distance(trans.position, box.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestBox = box;
            }
        }
        if (closestBox != null)
        {
            return closestBox;
        }
        else
        {
            // 박스가 없으면 Hero를 반환
            if(Hero.B_Alive)
                    return Hero;
            else
            {
                return null;
            }
        }
            
    }

    //박스 터젔을때 재 정렬
    Coroutine rootBox;
    public void BoxDie(Box box)
    {
        if(boxes.Contains(box))
        {
            boxes.Remove(box);
            if (rootBox != null)
                StopCoroutine(rootBox);
            rootBox = StartCoroutine(SetBoxInfo());
        }

        
    }
    public void HeroDie()
    {
        _gameManager.GameEnd();
        
    }


    // 기존 코드 교체: 박스와 Hero의 위치를 Lerp로 서서히 이동
    IEnumerator SetBoxInfo()
    {
        const float lerpDuration = 0.3f;
        float elapsed = 0f;

        // 현재 위치 저장
        Vector3[] startBoxPositions = new Vector3[boxes.Count];
        Vector3[] targetBoxPositions = new Vector3[boxes.Count];
        for (int i = 0; i < boxes.Count; i++)
        {
            startBoxPositions[i] = boxes[i].transform.localPosition;
            targetBoxPositions[i] = Data.BoxStartPositionY + (Data.BoxGapY * i);
        }
        Vector3 startHeroPos = Hero.transform.localPosition;
        Vector3 targetHeroPos;
        if (boxes.Count == 0)
        {
            targetHeroPos = Data.BoxStartPositionY;
        }
        else
        {
            targetHeroPos = Data.BoxStartPositionY + (Data.BoxGapY * (boxes.Count - 1)) + (Data.HeroGapY);
        }

        while (elapsed < lerpDuration)
        {
            float t = elapsed / lerpDuration;
            for (int i = 0; i < boxes.Count; i++)
            {
                boxes[i].transform.localPosition = Vector3.Lerp(startBoxPositions[i], targetBoxPositions[i], t);
            }
            Hero.transform.localPosition = Vector3.Lerp(startHeroPos, targetHeroPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // 마지막 위치 보정
        for (int i = 0; i < boxes.Count; i++)
        {
            boxes[i].transform.localPosition = targetBoxPositions[i];
        }
        Hero.transform.localPosition = targetHeroPos;
        StopCoroutine(rootBox);
    }
}
