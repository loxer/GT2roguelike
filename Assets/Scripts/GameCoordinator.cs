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
        bool newRoom = false;

        // player goes right
        if(player.transform.position.x > currentRoom.transform.position.x + 5)
        {
            List<GameObject> tempRoomList = new List<GameObject>();

            for(int i = 0; i < dungeonRooms.Length; i++)
            {
                if(currentRoom.transform.position.y == dungeonRooms[i].transform.position.y && 
                currentRoom.transform.position.x < dungeonRooms[i].transform.position.x)
                {
                    tempRoomList.Add(dungeonRooms[i]);
                }
            }

            if(tempRoomList.Count == 0)
            {
                Vector3 stayInRoomPosition = player.transform.position;
                stayInRoomPosition.x = currentRoom.transform.position.x + 5;
                player.transform.position = stayInRoomPosition;
            }
            else
            {
                newRoom = true;
                GameObject tempRoom = tempRoomList[0];

                for(int i = 1; i < tempRoomList.Count; i++)
                {
                    if(tempRoomList[i].transform.position.x < tempRoom.transform.position.x)
                    {
                        tempRoom = tempRoomList[i];
                    }
                }
                currentRoom = tempRoom;
            }
        }

        // player goes left
        if(player.transform.position.x < currentRoom.transform.position.x - 5)
        {
            List<GameObject> tempRoomList = new List<GameObject>();

            for(int i = 0; i < dungeonRooms.Length; i++)
            {
                if(currentRoom.transform.position.y == dungeonRooms[i].transform.position.y && 
                currentRoom.transform.position.x > dungeonRooms[i].transform.position.x)
                {
                    tempRoomList.Add(dungeonRooms[i]);
                }
            }

            if(tempRoomList.Count == 0)
            {
                Vector3 stayInRoomPosition = player.transform.position;
                stayInRoomPosition.x = currentRoom.transform.position.x - 5;
                player.transform.position = stayInRoomPosition;
            }
            else
            {
                newRoom = true;
                GameObject tempRoom = tempRoomList[0];

                for(int i = 1; i < tempRoomList.Count; i++)
                {
                    if(tempRoomList[i].transform.position.x > tempRoom.transform.position.x)
                    {
                        tempRoom = tempRoomList[i];
                    }
                }
                currentRoom = tempRoom;
            }
        }

        // player goes up
        if(player.transform.position.y > currentRoom.transform.position.y + 5)
        {
            List<GameObject> tempRoomList = new List<GameObject>();

            for(int i = 0; i < dungeonRooms.Length; i++)
            {
                if(currentRoom.transform.position.x == dungeonRooms[i].transform.position.x && 
                currentRoom.transform.position.y < dungeonRooms[i].transform.position.y)
                {
                    tempRoomList.Add(dungeonRooms[i]);
                }
            }

            if(tempRoomList.Count == 0)
            {
                Vector3 stayInRoomPosition = player.transform.position;
                stayInRoomPosition.y = currentRoom.transform.position.y + 5;
                player.transform.position = stayInRoomPosition;
            }
            else
            {
                newRoom = true;
                GameObject tempRoom = tempRoomList[0];

                for(int i = 1; i < tempRoomList.Count; i++)
                {
                    if(tempRoomList[i].transform.position.y < tempRoom.transform.position.y)
                    {
                        tempRoom = tempRoomList[i];
                    }
                }
                currentRoom = tempRoom;
            }
        }

        // player goes down
        if(player.transform.position.y < currentRoom.transform.position.y - 5)
        {
            List<GameObject> tempRoomList = new List<GameObject>();

            for(int i = 0; i < dungeonRooms.Length; i++)
            {
                if(currentRoom.transform.position.x == dungeonRooms[i].transform.position.x && 
                currentRoom.transform.position.y > dungeonRooms[i].transform.position.y)
                {
                    tempRoomList.Add(dungeonRooms[i]);
                }
            }

            if(tempRoomList.Count == 0)
            {
                Vector3 stayInRoomPosition = player.transform.position;
                stayInRoomPosition.y = currentRoom.transform.position.y - 5;
                player.transform.position = stayInRoomPosition;
            }
            else
            {
                newRoom = true;
                GameObject tempRoom = tempRoomList[0];

                for(int i = 1; i < tempRoomList.Count; i++)
                {
                    if(tempRoomList[i].transform.position.y > tempRoom.transform.position.y)
                    {
                        tempRoom = tempRoomList[i];
                    }
                }
                currentRoom = tempRoom;
            }
        }

        if(newRoom)
        {
            gameStatusChange = true;
            StartCoroutine(NewRoom());
        }
    }

    private IEnumerator NewRoom()
    {
        currentRoom.SetActive(true);
        StartCoroutine(cam.GoToRoom(currentRoom.transform.position));

        while(!cam.IsInPosition())                      // check regularly if the starting zoom has finished
        {
            yield return new WaitForSeconds(0.05f);
        }
            
        for(int i = 0; i < dungeonRooms.Length; i++)
        {            
            dungeonRooms[i].SetActive(false);           // make all rooms disappear
        }
        currentRoom.SetActive(true);

        yield return new WaitForSeconds(1f);            // give the player a second before something new happens
        gameStatusChange = true;
    }
}