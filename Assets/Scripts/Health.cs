using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health instance { get; private set; }
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(float value)
    {
        slider.value = Mathf.MoveTowards(slider.value, slider.value - value, 0.001f);
    }
}
