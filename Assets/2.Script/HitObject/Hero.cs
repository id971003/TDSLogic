using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero :  HitObject
{
    Truck truck;

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
        truck.BoxHeroDie();
    }
    public void SetUp(Truck truck)
    {
        b_Alive = true;
        this.truck = truck;
        b_Alive = true;
        MaxHp = Data.TruckHeroHp;
        Hp = MaxHp;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
