using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityBackGroundScroll : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField] private float defaultDistance; //기본거리
    [SerializeField] private float BackDestance; //돌아갈 거리
    [SerializeField] private Transform Target;
    [SerializeField] private List<GameObject> BackGroundList;
    [SerializeField] bool b_moveable; //자체적으로 이동하는 배경인지
    [SerializeField] private float currentFrontValue; //현재 앞쪽 배경의 위치


    private void Start()
    {
        _gameManager = GameManager.Instance;
        currentFrontValue = defaultDistance; //초기갑 설정
        if (b_moveable) //이동값있는 배경 이동하게
            StartCoroutine(b_MoveBackGround());
    }



    //자체적으로이동이동
    IEnumerator b_MoveBackGround() 
    {
        while (true)
        {
            yield return null;
            if (_gameManager.B_Move)
            {
                //배경이동
                foreach (var a in BackGroundList)
                {
                    a.transform.Translate(Data.backgroundSpeed);
                }
            }
        }
    }

    private void Update()
    {
        BackCheck();
    }
    
    //멀어지면 뒤로보내기
    void BackCheck()
    {

        if (Mathf.Abs(Target.transform.position.x - BackGroundList[0].transform.position.x) >= BackDestance) //거리를 넘어서면
        {
            var background = BackGroundList[0];
            background.transform.position = new Vector3(currentFrontValue + defaultDistance, background.transform.position.y, background.transform.position.z);
            currentFrontValue = background.transform.position.x; //현재 앞쪽 배경의 위치 업데이트
            BackGroundList.MoveToEnd(0);
        }
    }

}
