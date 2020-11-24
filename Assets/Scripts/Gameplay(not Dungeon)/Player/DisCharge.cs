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
            StartCoroutine(GameOver());
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
    
    private IEnumerator GameOver()
    {
        gameCoordinator.PlayerDead();

        yield return new WaitForSeconds(2f);            // give the player a moment to realize what just happened
        
        this.gameObject.SetActive(false);
        battery.gameObject.SetActive(false);
    }

    public void GameStarted()
    {
        this.gameObject.SetActive(true);
        battery.gameObject.SetActive(true);
        battery.value = 100;
    }

    public void SetGameCoordinator(GameCoordinator gameCoordinator)
    {
        this.gameCoordinator = gameCoordinator;
    }
}
