using Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.CullingGroup;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;



enum EMONSTERSTATE
{
    RUN,
    ATTACK,
}


public class Monster : HitObject
{
    GameManager gameManager;

    [Header("Monster Components")]
    [SerializeField] private GameObject view;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private Transform target;
    [SerializeField] Animator anim;
    


    Coroutine rootMove;
    Coroutine rootAttack;
    Coroutine rootJump;

    [Header("Monster Stats")]

    [SerializeField] private float MoveSpeed;
    [SerializeField] private float MaxHp;
    [SerializeField] private float Hp;
    [SerializeField] private float Dmg;



    [Header("Monster Data")]
    [SerializeField] private bool b_Alive;
    [SerializeField] private int layer = 0;
    [SerializeField] bool attacking;
    [SerializeField] bool canJump;
    [SerializeField] EMONSTERSTATE eMONSTERSTATE;
    [SerializeField] SpriteRenderer[] renderers=null;

    #region SetUp
    public void SetUp(int Layer, Transform target)
    {
        if(gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        renderers = null;

        Dmg = Data.MonsterDmg;
        canJump = true;
        attacking = false;
        layer = Layer;
        this.target = target;
        MoveSpeed = UnityEngine.Random.Range(Data.EnemySpeed_Min, Data.EnemySpeed_Max);
        gameObject.layer = LayerMask.NameToLayer(Data.LayerName[Layer]);
        

        SetSortingLayerRecursively(view, Data.LayerName[Layer]);
        StatChange(EMONSTERSTATE.RUN);

        if (rootMove != null)
            StopCoroutine(rootMove);
        rootMove = StartCoroutine(MoveRoot());

        if (rootAttack != null)
            StopCoroutine(rootAttack);
        rootAttack = StartCoroutine(CheckAttackRoot());   
    }

    public void SetSortingLayerRecursively(GameObject root, string sortingLayerName)
    {

        if (renderers == null)
        {
            renderers = root.GetComponentsInChildren<SpriteRenderer>(true); // 비활성 포함
        }


        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.sortingLayerName = sortingLayerName;
        }
    }
    #endregion



    #region Root
    IEnumerator MoveRoot()
    {
        while (true)
        {
            yield return null;
            if (!attacking)
            {
                view.transform.position += new Vector3(-MoveSpeed * Time.deltaTime, 0, 0);
            }
        }
    }
    IEnumerator CheckAttackRoot()
    {
        while (true)
        {
            if (!attacking)
            {
                CheckAttack();
            }
            yield return null;
        }
    }
    IEnumerator JumpRoot()
    {
        canJump = false;
        view.GetComponent<Rigidbody2D>().AddForce(Data.EenmyJumpVector);
        yield return Data.MonsterJumpDelay;
        canJump = true;
    }
    #endregion
    void StatChange(EMONSTERSTATE stat)
    {
        eMONSTERSTATE = stat;
        switch (eMONSTERSTATE)
        {
            case EMONSTERSTATE.RUN:
                attacking = false;
                //WALKANIM
                break;
            case EMONSTERSTATE.ATTACK:
                attacking = transform;
                //ATTACKANIM
                break;
            default:
                break;
        }
        AnimStatChange(stat);
    }
    void AnimStatChange(EMONSTERSTATE stat)
    {
        anim.SetTrigger(stat.ToString());
    }
    void CheckAttack()
    {
        if (transform.position.x - target.transform.position.x < Data.MonsterAttackDistance)
        {
            StatChange(EMONSTERSTATE.ATTACK);
        }
        else
        {
            StatChange(EMONSTERSTATE.RUN);
        }
    }

    public void OnAttack()
    {
        var box = gameManager.GetCloseBox(transform);
        if(box==null)
        {
            return;
        }
        box.Hit(Dmg);
    }
    public override void Hit(float Dmg)
    {
    }
    public override void Die()
    {

        RemoveObject();
    }
    public void RemoveObject()
    {

    }



    private void Update()
    {
        CheckJump();
        if (Input.GetKey(KeyCode.A))
        {
            Die();
        }
    }


    #region Jump
    public void CheckJump()
    {
        if (!canJump)
            return;
        if (attacking)
            return;
        RaycastHit2D[] hits = Physics2D.RaycastAll(collider2D.bounds.center, Vector2.left, Data.checkDistance);

#if UNITY_EDITOR
        Debug.DrawRay(collider2D.bounds.center, Vector2.left * Data.checkDistance, Color.red);
#endif

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == gameObject.layer && hit.collider.gameObject != gameObject)
                {

                    RaycastHit2D[] hits2 = Physics2D.RaycastAll(collider2D.bounds.center, Vector2.up, Data.checkDistance2);
                    Debug.DrawRay(collider2D.bounds.center, Vector2.up * Data.checkDistance2, Color.green);
                    foreach (RaycastHit2D hit2 in hits2)
                    {
                        if (hit2.collider != null)
                        {
                            if (hit2.collider.gameObject.layer == gameObject.layer && hit2.collider.gameObject != gameObject)
                            {
                                return; //위에 몬스터가 있으면 점프안함
                            }
                        }
                    }
                    if (rootJump != null)
                        StopCoroutine(rootJump);
                    rootJump = StartCoroutine(JumpRoot());
                }
            }
        }
    }
    //최대 높이 넘어가면 JUMP 안되게
    

    #endregion

}
