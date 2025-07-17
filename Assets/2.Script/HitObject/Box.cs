using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Box :  HitObject
{
    Coroutine rootHitRoot;
    [SerializeField] SpriteRenderer sprite;
    
    Truck truck;
    public void SetUp(Truck truck)
    {
        this.truck = truck;
        b_Alive = true;
        MaxHp = Data.TruckHeroHp;
        Hp = MaxHp;
        hpSlider.SetUp();

        base.SetUpMaterial();
    }

    
    public override void Hit(float Dmg)
    {
        base.Hit(Dmg);
        if (!b_Alive)
            return;
        if (rootHitRoot != null)
            StopCoroutine(rootHitRoot);
        rootHitRoot = StartCoroutine(HitRoot());

    }

    protected override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
        truck.BoxDie(this); 
    }
}
