using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitObject  : Poolable
{
    [SerializeField] protected float HitColorAmount;
    [SerializeField] protected SpriteRenderer[] renderers = null;
    [SerializeField] protected Coroutine rootHit;
    [SerializeField] protected Material instancedMaterial;


    public bool B_Alive => b_Alive;
    [SerializeField] protected bool b_Alive = true;
    [SerializeField] protected float MaxHp;
    [SerializeField] protected float Hp;

    [SerializeField] protected HpSlider hpSlider; 

    
    public virtual void SetUpMaterial()
    {
        
        if (renderers== null || renderers.Length == 0)
        {
            instancedMaterial = new Material(ResourceManager.Instance.material);
            renderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true); // 비활성 포함
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.material = instancedMaterial;
            }
            instancedMaterial.SetFloat("_HitAmount", 0);
        }
        else
        {
            instancedMaterial = new Material(ResourceManager.Instance.material);
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.material = instancedMaterial;
            }
            instancedMaterial.SetFloat("_HitAmount", 0);
        }
        
    }
    public virtual void Hit(float Dmg)
    {
        if (!B_Alive)
            return;
        Hp -= Dmg;
        if (Hp <= 0)
        {
            Die();
            return;
        }
        
        hpSlider.SetValue(Hp / MaxHp);
    }
    protected virtual void Die()
    {
        b_Alive = false;
    }
    protected IEnumerator HitRoot()
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
}
