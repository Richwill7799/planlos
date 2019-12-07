using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UIElements.Slider;

public class Health : MonoBehaviour
{
    //public static Health instance { get; private set; }
    
    public UnityEngine.UIElements.Slider slider2;

    private float val = 1.0f;

    private float sadjbl = 1.0f;
    // Start is called before the first frame update
    void Start() {
        slider2.value = 1f;
        //val = 1f;
        //slider2 = GetComponent<Slider>();
    }

    // Update is called once per frame
    public void LateUpdate() {
        //slider2.value = sadjbl;
        Debug.Log("Val: "+val + " and "+ sadjbl);
    }

    public void SetValue(float valuestogive) {

        Debug.Log("Value given: " + valuestogive);
        //slider2.value = value;
        val = valuestogive;
        sadjbl = valuestogive;
        Debug.Log("Val: "+val);

        //slider.value = Mathf.MoveTowards(slider.value, value, 10f);
        //Debug.Log("update health value: " + slider.value);
    }
}
