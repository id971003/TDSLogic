using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class Monster : Poolable
{
    [SerializeField] private GameObject view;
    [SerializeField] private CircleCollider2D collider2D;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject targe2;
    [SerializeField] private int layer = 0;


    Coroutine rootMove;
    Coroutine rootJump;


    private float MoveSpeed;
    private float MaxHp;
    private float Hp;

    bool canJump;

    public void SetUp(int Layer)
    {
        canJump = true;
        layer = Layer;
        collider2D.radius = 0.4f;
        gameObject.layer = LayerMask.NameToLayer(Data.LayerName[Layer]);
        SetSortingLayerRecursively(view,Data.LayerName[Layer]);
        if (rootMove != null)
            StopCoroutine(rootMove);
        rootMove = StartCoroutine(MoveRoot());


        MoveSpeed= Random.Range(Data.EnemySpeed_Min, Data.EnemySpeed_Max);
    }

    public void SetSortingLayerRecursively(GameObject root, string sortingLayerName)
    {
        //TODO GC 해결
        SpriteRenderer[] renderers = root.GetComponentsInChildren<SpriteRenderer>(true); // 비활성 포함

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.sortingLayerName = sortingLayerName;
        }
    }

    IEnumerator MoveRoot()
    {
        while (true)
        {
            view.transform.position += new Vector3(-MoveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }
    
    private void Update()
    {
        CheckLay();
    }

    
    Vector2 direction = Vector2.left;
    public void CheckLay()
    {
        if (!canJump)
            return;
        RaycastHit2D[] hits = Physics2D.RaycastAll(collider2D .bounds.center, direction, Data.checkDistance);

#if UNITY_EDITOR
        Debug.DrawRay(collider2D.bounds.center, direction * Data.checkDistance, Color.red);
#endif

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == gameObject.layer && hit.collider.gameObject != gameObject)
                {
                    target= hit.collider.gameObject; 
                    if (rootJump != null)
                        StopCoroutine(rootJump);
                    rootJump = StartCoroutine(JumpRoot());
                }
            }
        }
    }
    //최대 높이 넘어가면 JUMP 안되게
    IEnumerator JumpRoot()
    {
        canJump = false;
        view.GetComponent<Rigidbody2D>().AddForce(Data.EenmyJumpVector);
        yield return Data.MonsterJumpDelay;
        canJump = true;
    }

    

}
