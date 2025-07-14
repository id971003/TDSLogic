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
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject targe2;
    [SerializeField] private int layer = 0;


    Coroutine rootMove;
    Coroutine rootJump;

    bool canJump;

    public void SetUp(int Layer)
    {
        canJump = true;
        layer = Layer;
        gameObject.layer = LayerMask.NameToLayer(Data.LayerName[Layer]);
        SetSortingLayerRecursively(view,Data.LayerName[Layer]);
        if (rootMove != null)
            StopCoroutine(rootMove);
        rootMove = StartCoroutine(MoveRoot());
    }

    public void SetSortingLayerRecursively(GameObject root, string sortingLayerName)
    {
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
            view.transform.position += new Vector3(-Data.EnemySpeed * Time.deltaTime, 0, 0);
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
    IEnumerator JumpRoot()
    {
        canJump = false;
        view.GetComponent<Rigidbody2D>().AddForce(Data.EenmyJumpVector);
        yield return Data.MonsterJumpDelay;
        canJump = true;
    }

    

}
