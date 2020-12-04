using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisCharge : MonoBehaviour
{
    public Slider battery = default;
    private GameCoordinator gameCoordinator;
    private Vector3 playerStartingPosition;

    void Start()
    {
        playerStartingPosition = transform.position;
    }

    void Update()
    {
        if (battery.value <= 0 || Game.won)
        {
            StartCoroutine(GameOver());
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Akkufresser") || other.CompareTag("PopUp"))
        {
            // Debug.Log("-5");
            battery.value -= 5f;
        }
        if (other.CompareTag("Boss"))
        {
            // Debug.Log("-10");
            battery.value -= 10f;
        }
        if (other.CompareTag("Steckdose"))
        {
            // Debug.Log("+5");
            battery.value += 5f;
        }
    }
    
    private IEnumerator GameOver()
    {
        gameCoordinator.GameEnded();

        yield return new WaitForSeconds(2f);            // give the player a moment to realize what just happened
        
        transform.position = playerStartingPosition;
        battery.gameObject.SetActive(false);
    }

    public void GameStarted()
    {
        battery.gameObject.SetActive(true);
        battery.value = 100;
    }

    public void SetGameCoordinator(GameCoordinator gameCoordinator)
    {
        this.gameCoordinator = gameCoordinator;
    }
}
