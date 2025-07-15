using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box :  HitObject
{

    public bool B_Alive => b_Alive; 
    [SerializeField] private bool b_Alive = true;
    [SerializeField] private float MaxHp;
    [SerializeField] private float Hp;
    [SerializeField] private GameObject go_hpSlider;
    public void SetUp()
    {
        b_Alive = true;

    }
    public override void Hit(float Dmg)
    {
        Debug.Log(gameObject.name + " Hit " + Dmg); 
    }

    public override void Die()
    {
        
    }
}
