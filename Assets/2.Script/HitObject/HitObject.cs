using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitObject  : Poolable
{
    public bool B_Alive => b_Alive;
    [SerializeField] protected bool b_Alive = true;
    [SerializeField] protected float MaxHp;
    [SerializeField] protected float Hp;

    [SerializeField] protected HpSlider hpSlider; 
    public virtual void Hit(float Dmg)
    {
        Hp -= Dmg;
        if (Hp <= 0)
        {
            Die();
            return;
        }

        hpSlider.SetValue(Hp / MaxHp);
    }
    public virtual void Die()
    {
        TryPool(gameObject);
    }
}
