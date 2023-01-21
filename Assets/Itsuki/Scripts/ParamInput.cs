using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ParamInput : MonoBehaviour
{
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] Text param;
    float value;
    public void ParamChange()
    {
        value = scrollbar.value;
        param.text = (value * 100f).ToString();
        Debug.Log("value changed : " + value * 100);
        //scrollbar.OnValueChangedAsObservable.Subscribe(value);
    }
}
