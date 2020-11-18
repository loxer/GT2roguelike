using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    // https://www.youtube.com/watch?v=BLfNP4Sc_iA
    
    [SerializeField] private Slider battery = default;
    [SerializeField] private Gradient gradient = default; 
    [SerializeField] private Image fill = default; 

    void Start()
    {
        battery.value = 100; // fully loaded
        fill.color =  gradient.Evaluate(1f); // gradient is green in the beginning -> 100% 

        InvokeRepeating("DecreaseBattery", 1.0f, 1.0f); //battery loses 1% of power every second 
    }

    private void DecreaseBattery(){
        if(battery.value > 0) {
            battery.value -= 1;
        }
        fill.color =  gradient.Evaluate(battery.normalizedValue); //normalized Value returns a value between 0 and 1 
    }

    public float GetBattery()
    {
        return battery.value;
    }
    void Update()
    {
        // for testing purposes
    /*    battery.value -= 1; 
        fill.color =  gradient.Evaluate(battery.normalizedValue);*/

    }
}
