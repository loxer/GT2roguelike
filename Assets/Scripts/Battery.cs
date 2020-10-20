using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    [SerializeField] private Slider battery = default;

    void Start()
    {
        battery.value = 100; // fully loaded
        InvokeRepeating("decreaseBattery", 1.0f, 1.0f);

    }

    void decreaseBattery(){
        if(battery.value > 0) {
            battery.value -= 1;
        }
    }
    void Update()
    {
        battery.value -= 1; 
    }
}
