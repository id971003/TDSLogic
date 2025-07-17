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
    GameManager _gameManager;

    [Header("Monster Components")]
    [SerializeField] private GameObject view;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private Transform target;
    [SerializeField] Animator anim;



    Coroutine rootMove;
    Coroutine rootAttack;
    Coroutine rootJump;
    Coroutine rootHitRoot;

    [Header("Monster Stats")]

    [SerializeField] private float MoveSpeed;
    [SerializeField] private float Dmg;




    [SerializeField] private float HitColorAmount;
    [Header("Monster Data")]
    [SerializeField] private int layer = 0;
    [SerializeField] bool attacking;
    [SerializeField] bool canJump;
    [SerializeField]
    Material instancedMaterial;
    [SerializeField] EMONSTERSTATE eMONSTERSTATE;

    [SerializeField] SpriteRenderer[] renderers = null;

    #region SetUp
    public void SetUp(int Layer, Transform target, Material mat)
    {
        if (_gameManager == null)
        {
            _gameManager = GameManager.Instance;
        }

        renderers = null;

        Dmg = Data.MonsterDmg;
        canJump = true;
        attacking = false;
        layer = Layer;
        this.target = target;
        MaxHp = Data.MonsterMaxHp;
        Hp = MaxHp;
        b_Alive = true;
        MoveSpeed = Data.EnemySpeed;
        gameObject.layer = LayerMask.NameToLayer(Data.LayerName[layer]);
        hpSlider.SetUp();

        SetSortingLayerRecursively(view, Data.LayerName[layer], mat);
        StatChange(EMONSTERSTATE.RUN);

        if (rootMove != null)
            StopCoroutine(rootMove);
        rootMove = StartCoroutine(MoveRoot());

        if (rootAttack != null)
            StopCoroutine(rootAttack);
        rootAttack = StartCoroutine(CheckAttackRoot());

        if (rootHitRoot != null)
            StopCoroutine(rootHitRoot);



        HitColorAmount = 0;
        instancedMaterial.SetFloat("_HitAmount", 0);
    }

    public void SetSortingLayerRecursively(GameObject root, string sortingLayerName, Material mat)
    {
        instancedMaterial = new Material(mat);
        if (renderers == null)
        {
            renderers = root.GetComponentsInChildren<SpriteRenderer>(true); // 비활성 포함
        }


        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.material = instancedMaterial;
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

    #region Stat
    void StatChange(EMONSTERSTATE stat)
    {
        eMONSTERSTATE = stat;
        switch (eMONSTERSTATE)
        {
            case EMONSTERSTATE.RUN:
                attacking = false;
                break;
            case EMONSTERSTATE.ATTACK:
                attacking = true;
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
    #endregion

    #region Attack
    void CheckAttack()
    {
        if (!_gameManager.B_GameStart)
        {
            StatChange(EMONSTERSTATE.RUN);
            return;
        }

        if (transform.position.x - target.transform.position.x < Data.MonsterAttackDistance)
        {
            StatChange(EMONSTERSTATE.ATTACK);
        }
        else
        {
            StatChange(EMONSTERSTATE.RUN);
        }
    }
    #endregion

    #region  AnimationEvent
    public void OnAttack()
    {
        if (!b_Alive)
            return;
        var box = _gameManager.GetCloseBox(transform);
        if (box == null)
        {
            return;
        }
        box.Hit(Dmg);
    }
    public void EndAttack()
    {
        if (!b_Alive)
            return;
        attacking = false;
        CheckAttack();
    }
    #endregion

    #region HitAndDie
    public override void Hit(float Dmg)
    {
        base.Hit(Dmg);

        if (rootHitRoot != null)
            StopCoroutine(rootHitRoot);
        if (!b_Alive)
            return;
        rootHitRoot = StartCoroutine(HitRoot());
    }
    IEnumerator HitRoot()
    {

        HitColorAmount = 0.8f;
        instancedMaterial.SetFloat("_HitAmount", HitColorAmount);

        while (true)
        {
            HitColorAmount -= 0.02f;
            instancedMaterial.SetFloat("_HitAmount", HitColorAmount);
            if (HitColorAmount <= 0)
            {
                HitColorAmount = 0;
                break;
            }
            yield return null;
        }
        instancedMaterial.SetFloat("_HitAmount", HitColorAmount);
    }
    public override void Die()
    {
        base.Die();
        StopAllCoroutines();
        _gameManager.RemoveMonster(this);
    }
    #endregion

    #region Jump
    private void Update()
    {
        CheckJump();
    }
    public void CheckJump()
    {
        if (!canJump)
            return;
        if (attacking)
            return;
        

#if UNITY_EDITOR
        Debug.DrawRay(collider2D.bounds.center, Vector2.left * Data.checkMonsterDistanceFront, Color.red);
        Debug.DrawRay(collider2D.bounds.center, Vector2.up * Data.checkMonstserDistanceUp, Color.green);
        Debug.DrawRay(collider2D.bounds.center, Data.checkLeftUpVector * Data.checkMonstserDistanceLeftUp, Color.blue);
#endif

        //위에 몬스터있는지체크
        RaycastHit2D[] upLayers = Physics2D.RaycastAll(collider2D.bounds.center, Vector2.up, Data.checkMonstserDistanceUp);

        foreach (RaycastHit2D uplayer in upLayers)
        {
            if (uplayer.collider != null)
            {
                if (uplayer.collider.gameObject.layer == gameObject.layer && uplayer.collider.gameObject != gameObject)
                {
                    return; //위에 몬스터가 있으면 점프안함
                }
            }
        }

        //대각선에 있는지체크
        RaycastHit2D[] upLeftLayers = Physics2D.RaycastAll(collider2D.bounds.center, Data.checkLeftUpVector, Data.checkMonstserDistanceLeftUp);

        foreach (RaycastHit2D upLeftlayer in upLeftLayers)
        {
            if (upLeftlayer.collider != null)
            {
                if (upLeftlayer.collider.gameObject.layer == gameObject.layer && upLeftlayer.collider.gameObject != gameObject)
                {
                    return; //위에 몬스터가 있으면 점프안함
                }
            }
        }


        //앞에있는지체크
        RaycastHit2D[] frontlays = Physics2D.RaycastAll(collider2D.bounds.center, Vector2.left, Data.checkMonsterDistanceFront);
        foreach (RaycastHit2D front in frontlays)
        {
            if (front.collider != null)
            {
                if (_gameManager.B_GameStart)
                {
                    if (front.collider.gameObject.layer == gameObject.layer && front.collider.gameObject != gameObject)
                    {
                        if (rootJump != null)
                            StopCoroutine(rootJump);
                        rootJump = StartCoroutine(JumpRoot());
                    }
                }
                else
                {
                    if ((front.collider.gameObject.layer == gameObject.layer|| front.collider.gameObject.layer == Data.TruckLayerNum)
                        && front.collider.gameObject != gameObject)
                    {
                        if (rootJump != null)
                            StopCoroutine(rootJump);
                        rootJump = StartCoroutine(JumpRoot());
                    }
                }
            }
        }
    }
    #endregion

}
