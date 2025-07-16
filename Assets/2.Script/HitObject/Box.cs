using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Box :  HitObject
{
    Coroutine rootHitRoot;
    [SerializeField] SpriteRenderer sprite;
    Material instancedMaterial;
    float HitColorAmount = 0;
    Truck truck;
    public void SetUp(Truck truck)
    {
        this.truck = truck;
        b_Alive = true;
        MaxHp = Data.TruckHeroHp;
        Hp = MaxHp;
        hpSlider.SetUp();
        instancedMaterial = new Material(ResourceManager.Instance.material);
        sprite.material = instancedMaterial;
        instancedMaterial.SetFloat("_HitAmount", 0);
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
        Debug.Log("Hit Root Finished");
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

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
        truck.BoxDie(this); 
    }
}
