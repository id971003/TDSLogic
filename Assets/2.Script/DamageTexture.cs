using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DamageTexture : Poolable
{
    [SerializeField] private Rigidbody2D rg2d;
    [SerializeField] private Text text;
    
    Coroutine rootTexture;
    public void SetUp(Transform trans,float Dmg)
    {

        transform.position=trans.position;
        text.text = Dmg.ToString();
        text.color = Color.white;
        text.transform.localScale = Vector3.one;
        if (rootTexture != null)
        {
            StopCoroutine(rootTexture);
        }
        rootTexture = StartCoroutine(TextureRoot());
    }
    IEnumerator TextureRoot()
    {
        rg2d.AddForce(new Vector2(Random.Range(-0.25f, 0.25f),7), ForceMode2D.Impulse);
        while(true)
        {
            text.transform.localScale = Vector3.Lerp(text.transform.localScale, Vector3.zero, Time.deltaTime );
            text.color = Color.Lerp(Color.white, Color.clear, Time.deltaTime );
            if (text.transform.localScale.x < 0.5f)
            {
                break;
            }
            yield return null;
        }
        text.transform.localScale = Vector3.zero;
        text.color = Color.clear;
        Poolable.TryPool(gameObject);

    }
}
