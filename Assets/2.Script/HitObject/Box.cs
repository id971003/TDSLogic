using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box :  HitObject
{
    Truck truck;
    public void SetUp(Truck truck)
    {
        this.truck = truck;
        b_Alive = true;
        MaxHp = Data.TruckHeroHp;
        Hp = MaxHp;
        hpSlider.SetUp();
    }
    public override void Hit(float Dmg)
    {
        base.Hit(Dmg);
    }

    public override void Die()
    {
        gameObject.SetActive(false);
        truck.BoxDie(this); 
    }
}
