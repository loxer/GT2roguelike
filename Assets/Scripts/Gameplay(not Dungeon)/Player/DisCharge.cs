using System.Collections;
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
        if (battery.value <= 0)
        {
            StartCoroutine(GameOver());
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Akkufresser") || other.CompareTag("PopUp"))
        {
            battery.value -= 5f;
        }
        if (other.CompareTag("Boss"))
        {
            battery.value -= 10f;
        }
        if (other.CompareTag("Steckdose"))
        {
            battery.value += 5f;
        }       
    }
    
    private IEnumerator GameOver()
    {
        gameCoordinator.PlayerDead();
        // give the player a moment to realize what just happened
        yield return new WaitForSeconds(2f);            
        
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
