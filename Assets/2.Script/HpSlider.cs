using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    [SerializeField] private Slider hpslider;
    [SerializeField] private float TargetValue;

    Coroutine rootSet;
    public void Start() 
    {
        hpslider.value = 1;
        TargetValue = 1;
        hpslider.gameObject.SetActive(false);
    }
    public void SetValue(float value)
    {
        if(rootSet==null)
        {
            rootSet = StartCoroutine(SetRoot());
            Debug.Log("ROOTSTARRT");
            hpslider.gameObject.SetActive(true);
        }
        TargetValue = value;
        hpslider.value = Mathf.Lerp(hpslider.value, TargetValue, Time.deltaTime * 5);
        
    }
    IEnumerator SetRoot()
    {
        while(true)
        {
            hpslider.value = Mathf.Lerp(hpslider.value, TargetValue, Time.deltaTime * 5);
            yield return null;
        }
    }
}
