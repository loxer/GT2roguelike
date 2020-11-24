using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisCharge : MonoBehaviour
{
    public Slider battery = default;
    private GameCoordinator gameCoordinator;

    void Update()
    {
        if (battery.value <= 0)
        {
            GameOver();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Akkufresser") || other.CompareTag("PopUp"))
        {
            Debug.Log("-5");
            battery.value -= 5f;
        }
        if (other.CompareTag("Boss"))
        {
            Debug.Log("-10");
            battery.value -= 10f;
        }
        if (other.CompareTag("Steckdose"))
        {
            Debug.Log("+5");
            battery.value += 5f;
        }       
    }
    
    private void GameOver()
    {
        this.gameObject.SetActive(false);
        gameCoordinator.PlayerDead();
    }

    public void GameStarted()
    {
        this.gameObject.SetActive(true);
        battery.value = 100;
        Debug.Log("Get full battery");
    }

    public void SetGameCoordinator(GameCoordinator gameCoordinator)
    {
        this.gameCoordinator = gameCoordinator;
    }
}
