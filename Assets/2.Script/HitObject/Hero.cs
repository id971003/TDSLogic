using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero :  HitObject
{
    Truck truck;

    [SerializeField] private Transform ShotPosition;
    [SerializeField] private GameObject Buttlet;
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
        GameObject buttlet = Poolable.TryGetPoolable(Buttlet);   //TODO  Pooling
        if (buttlet == null) return;
        buttlet.transform.position = ShotPosition.position;
        buttlet.GetComponent<Bullet>().SetUp(ShotDirection,Data.BulletDamage);

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
