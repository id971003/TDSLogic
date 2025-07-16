using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Poolable
{
    [SerializeField] private float Dmg;
    Vector3 direction;
    Coroutine rootMove;
    public void SetUp(Vector3 dir,float dmg)
    {
        direction = dir;
        Dmg = dmg;
        if (rootMove != null)
            StopCoroutine(rootMove);
        rootMove= StartCoroutine(MoveRoot());

    }
    IEnumerator MoveRoot()
    {
        float lifeTime = Data.BulletLifeTime;
        while (true)
        {
            transform.position += direction.normalized * Data.BulletSpeed * Time.deltaTime;
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Remove();
                yield break;
            }
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            HitObject hitObject = collision.GetComponentNoGarbage<HitObject>();
            hitObject.Hit(Dmg);
            Remove();
        }
    }

    public void Remove()
    {
        if (rootMove != null)
            StopCoroutine(rootMove);
        TryPool(gameObject);
    }
}
