using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisCharge : MonoBehaviour
{
    [SerializeField] private Slider battery = default;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Akkufresser"))
        {
            Debug.Log("-5");
            battery.value -= 5f;
        }
        if (other.CompareTag("Steckdose"))
        {
            Debug.Log("+5");
            battery.value += 5f;
        }

        if (battery.value <= 0)
        {
            GameOver();
        }
    }
    
    private void GameOver()
    {
        Destroy(gameObject);
        Debug.Log("You Lost :( ");
    }
}
