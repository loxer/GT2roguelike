using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    private CameraControl cam;
    private GameObject player;
    private GameObject[] dungeonRooms;
    private GameObject currentRoom;
    
    private bool gameStatusChange = false;
    private bool gameRunning = false;
    

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraControl>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(gameStatusChange)
        {
            if(gameRunning)
            {
                DisablePlayer();
                gameRunning = false;
            }
            else
            {
                EnablePlayer();
                gameRunning = true;
            }

            gameStatusChange = false;
        }

        if(gameRunning)
        {
            CheckForRoomChange();
        }        
    }
    
    public void DungeonGenerationFinished(GameObject[] dungeonRooms, GameObject dungeonGenerator)
    {
        dungeonGenerator.gameObject.SetActive(false);
        this.dungeonRooms = dungeonRooms;
        currentRoom = dungeonRooms[0];
        StartCoroutine(PrepareCameraPosition());        
    }

    private IEnumerator PrepareCameraPosition()
    {
        StartCoroutine(cam.StartingZoom(currentRoom.transform.position)); // Start room

        while(!cam.IsInPosition())                      // check regularly if the starting zoom has finished
        {
            yield return new WaitForSeconds(0.05f);
        }        
        
        yield return new WaitForSeconds(1f);            // give the player a second before something new happens
        
        for(int i = 1; i < dungeonRooms.Length; i++)
        {
            dungeonRooms[i].SetActive(false);           // make all rooms disappear (except the first one)
        }

        player.transform.position = currentRoom.transform.position;
        EnablePlayer();
        gameStatusChange = true;
    }

    private void EnablePlayer()
    {        
        player.GetComponent<WalkingCycle>().enabled = true;
        player.GetComponent<PlayerShoot>().enabled = true;        
    }

    private void DisablePlayer()
    {        
        player.GetComponent<WalkingCycle>().enabled = false;
        player.GetComponent<PlayerShoot>().enabled = false;        
    }

    private void CheckForRoomChange()
    {
        // player goes right
        if(player.transform.position.x > currentRoom.transform.position.x + 5)
        {

            gameStatusChange = true;
            print("player goes right");
        }

        // player goes left
        if(player.transform.position.x < currentRoom.transform.position.x - 5)
        {

            gameStatusChange = true;
            print("player goes left");
        }

        // player goes up
        if(player.transform.position.y > currentRoom.transform.position.y + 5)
        {

            gameStatusChange = true;
            print("player goes up");
        }

        // player goes down
        if(player.transform.position.y < currentRoom.transform.position.y - 5)
        {

            gameStatusChange = true;
            print("player goes down");
        }
    }    
}