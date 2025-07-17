using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityBackGroundScroll : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField] private float defaultDistance; //�⺻�Ÿ�
    [SerializeField] private float BackDestance; //���ư� �Ÿ�
    [SerializeField] private Transform Target;
    [SerializeField] private List<GameObject> BackGroundList;
    [SerializeField] bool b_moveable; //��ü������ �̵��ϴ� �������
    [SerializeField] private float currentFrontValue; //���� ���� ����� ��ġ


    private void Start()
    {
        _gameManager = GameManager.Instance;
        currentFrontValue = defaultDistance; //�ʱⰩ ����
        if (b_moveable) //�̵����ִ� ��� �̵��ϰ�
            StartCoroutine(b_MoveBackGround());
    }



    //��ü�������̵��̵�
    IEnumerator b_MoveBackGround() 
    {
        while (true)
        {
            yield return null;
            if (_gameManager.B_Move)
            {
                //����̵�
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
    
    //�־����� �ڷκ�����
    void BackCheck()
    {

        if (Mathf.Abs(Target.transform.position.x - BackGroundList[0].transform.position.x) >= BackDestance) //�Ÿ��� �Ѿ��
        {
            var background = BackGroundList[0];
            background.transform.position = new Vector3(currentFrontValue + defaultDistance, background.transform.position.y, background.transform.position.z);
            currentFrontValue = background.transform.position.x; //���� ���� ����� ��ġ ������Ʈ
            BackGroundList.MoveToEnd(0);
        }
    }

}
