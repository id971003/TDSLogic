using Lib;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Bullet : Poolable
{
    private ResourceManager _resourceManager;
    [SerializeField] private float Dmg;
    Vector3 direction;
    Coroutine rootMove;
    public void SetUp(Vector3 dir,float dmg)
    {

        if(_resourceManager == null)
            _resourceManager = ResourceManager.Instance;

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
        if(collision.CompareTag(Data.MonsterTag))
        {
            var bullet=Poolable.TryGetPoolable(_resourceManager.DamageTextures);
            bullet.GetComponentNoGarbage<DamageTexture>().SetUp(transform, Dmg);
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
