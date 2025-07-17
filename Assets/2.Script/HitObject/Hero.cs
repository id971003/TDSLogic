using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hero : HitObject
{
    Truck truck;

    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private Transform Gun;
    [SerializeField] private Transform ShotPosition;
    [SerializeField] private GameObject prefab_bullet;
    [SerializeField] private Vector3 ShotDirection;
    
    Coroutine rootShot;
    



    public void SetUp(Truck truck)
    {
        b_Alive = true;
        instancedMaterial = new Material(ResourceManager.Instance.material);
        sprite.material = instancedMaterial;
        this.truck = truck;
        b_Alive = true;
        MaxHp = Data.TruckHeroHp;
        hpSlider.SetUp();
        Hp = MaxHp;
        base.SetUpMaterial();
        rootShot = StartCoroutine(ShotRoot());
    }

    public override void Hit(float Dmg)
    {
        Hp -= Dmg;
        if (Hp <= 0)
        {
            Die();
            return;
        }

        if (rootHit != null)
            StopCoroutine(rootHit);
        rootHit = StartCoroutine(HitRoot());
        hpSlider.SetValue(Hp / MaxHp);
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
    protected override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
        truck.HeroDie();
    }

    IEnumerator ShotRoot()
    {
        while (b_Alive)
        {
            yield return Data.TruckHeroShotDelay;
            Shot();
        }
    }
    void Shot()
    {
        if (!b_Alive) return;

        // 2D ��� ���� (z�� ȸ���� ����)
        Vector2 baseDir = new Vector2(ShotDirection.x, ShotDirection.y).normalized;

       
        GameObject bullet1 = Poolable.TryGetPoolable(prefab_bullet);
        bullet1.transform.position = ShotPosition.position;
        bullet1.GetComponentNoGarbage<Bullet>().SetUp(new Vector3(baseDir.x, baseDir.y, 0), Data.BulletDamage);

        float angle2 = UnityEngine.Random.Range(-3f, 3f);
        Vector2 ran2 = Quaternion.Euler(0, 0, angle2) * baseDir;
        GameObject bullet2 = Poolable.TryGetPoolable(prefab_bullet);
        bullet2.transform.position = ShotPosition.position;
        bullet2.GetComponentNoGarbage<Bullet>().SetUp(new Vector3(ran2.x, ran2.y, 0), Data.BulletDamage);
    }
    void GunRotate()
    {
        // 2D ���(z�� ����)���� ShotDirection�� �ٶ󺸰� ȸ��
        Vector2 dir = new Vector2(ShotDirection.x, ShotDirection.y);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Gun.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetRotate(Vector3 target)
    {
        if(target == null)
        {
            return;
        }
        // target�� ShotPosition ������ ���� ���� ���
        Vector2 dir = (target - ShotPosition.position);
        ShotDirection = dir.normalized;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GunRotate();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shot();
        }
    }
}
