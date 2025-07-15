using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HitObject  : Poolable
{
    public abstract void Hit(float Dmg);
    public abstract void Die();
}
