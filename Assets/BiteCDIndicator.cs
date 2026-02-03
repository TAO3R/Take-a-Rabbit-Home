using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiteCDIndicator : MonoBehaviour
{
    public Slider slider;

    public void SetMaxCD(float maxCD)
    {
        slider.maxValue = maxCD;
        slider.value = maxCD;
    }
    
    public void SetBiteCD(float CDRatio)
    {
        slider.value = CDRatio;
    }
}
