using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : HitObject
{
    Truck truck;

    [SerializeField] private Transform ShotPosition;
    [SerializeField] private GameObject prefab_bullet;
    [SerializeField] private Vector3 ShotDirection;

    public override void Hit(float Dmg)
    {
        Hp -= Dmg;
        if (Hp <= 0)
        {
            Die();
            return;
        }

        hpSlider.SetValue(Hp / MaxHp);
    }
    public override void Die()
    {
        gameObject.SetActive(false);
        truck.HeroDie();
    }
    public void SetUp(Truck truck)
    {
        b_Alive = true;
        this.truck = truck;
        b_Alive = true;
        MaxHp = Data.TruckHeroHp;
        Hp = MaxHp;
    }
    void Shot()
    {
        if (!b_Alive) return;

        // 2D 평면 기준 (z축 회전만 적용)
        Vector2 baseDir = new Vector2(ShotDirection.x, ShotDirection.y).normalized;

        float angle1 = UnityEngine.Random.Range(-10f, 10f);
        Vector2 ran1 = Quaternion.Euler(0, 0, angle1) * baseDir;
        GameObject bullet1 = Poolable.TryGetPoolable(prefab_bullet);
        bullet1.transform.position = ShotPosition.position;
        bullet1.GetComponent<Bullet>().SetUp(new Vector3(ran1.x, ran1.y, 0), Data.BulletDamage);

        float angle2 = UnityEngine.Random.Range(-10f, 10f);
        Vector2 ran2 = Quaternion.Euler(0, 0, angle2) * baseDir;
        GameObject bullet2 = Poolable.TryGetPoolable(prefab_bullet);
        bullet2.transform.position = ShotPosition.position;
        bullet2.GetComponent<Bullet>().SetUp(new Vector3(ran2.x, ran2.y, 0), Data.BulletDamage);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shot();
        }
    }
}
