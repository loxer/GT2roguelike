using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCoordinator : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab = default;
    private CameraControl cam;
    private GameObject player;
    private GameObject[] dungeonRooms;
    private GameObject currentRoom;
    private GameObject dungeonGenerator;   
    
    private bool gameStatusChange = false;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraControl>();
        player = GameObject.FindWithTag("Player");
        player.GetComponent<DisCharge>().SetGameCoordinator(this);        

        Game.isRunning = false;
        Game.won = false;
    }

    void Update()
    {
        if(gameStatusChange)
        {
            if(Game.isRunning)
            {
                DisablePlayer();
                Game.isRunning = false;
            }
            else
            {
                EnablePlayer();
                Game.isRunning = true;
            }

            gameStatusChange = false;
        }

        if(Game.isRunning)
        {
            CheckForRoomChange();
        }        
    }

    private void GameStatusChange()
    {
        gameStatusChange = true;
    }
    
    public void DungeonGenerationFinished(GameObject[] dungeonRooms, GameObject dungeonGenerator)
    {
        this.dungeonGenerator = dungeonGenerator;
        dungeonGenerator.SetActive(false);

        this.dungeonRooms = dungeonRooms;

        //spawn in first room of path
        currentRoom = dungeonRooms[0];
        Game.lastRoom = dungeonRooms[dungeonRooms.Length - 1];

        // boss spawns in last dungeon 
        GameObject[] enemyArray = Game.lastRoom.AddComponent<EnemySpawnPoint>().enemy = new GameObject[1];
        enemyArray[0] = bossPrefab;
        enemyArray[0].SetActive(true);                 // boss is now visible right from the beginning
        

        StartCoroutine(PrepareCameraPosition());
    }

    private IEnumerator PrepareCameraPosition()
    {
        cam.ZoomIn(currentRoom.transform.position);     // Start room

        while(!cam.IsInPosition())                      // check regularly if the starting zoom has finished
        {
            yield return new WaitForSeconds(0.05f);
        }
        
        yield return new WaitForSeconds(1f);            // give the player a second before something new happens
                
        for(int i = 0; i < dungeonRooms.Length; i++)
        {
            dungeonRooms[i].SetActive(false);           // make all rooms disappear (except the first one)
            dungeonRooms[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        }

        currentRoom.SetActive(true);
        player.transform.position = currentRoom.transform.position;
        player.GetComponent<DisCharge>().GameStarted();
        GameStatusChange();
    }

    private void EnablePlayer()
    {        
        player.GetComponent<RbMovement>().enabled = true;
        player.GetComponent<DisCharge>().enabled = true;  
        player.GetComponent<KeepCharacterOnScreen>().enabled = true;        

    }

    private void DisablePlayer()
    {        
        player.GetComponent<RbMovement>().enabled = false;
        player.GetComponent<DisCharge>().enabled = false;  
        player.GetComponent<KeepCharacterOnScreen>().enabled = false;       
    }

    public void GameEnded()
    {
        GameStatusChange();
        StartCoroutine(PrepareRestart());
    }


    private IEnumerator PrepareRestart()
    {
        DisablePlayer();
        
        yield return new WaitForSeconds(1f);            // give the player a second to realize what just happened

        for(int i = 0; i < dungeonRooms.Length; i++)
        {
            dungeonRooms[i].SetActive(true);
        }

        cam.ZoomOut();

        while(!cam.IsInPosition())                      // check regularly if the starting zoom has finished
        {
            yield return new WaitForSeconds(0.05f);
        }
       
        yield return new WaitForSeconds(1f);            // give the player a second before something new happens
        
        if(Game.won)
        {
            SceneManager.LoadScene("Win");
        }
        else
        {
            SceneManager.LoadScene("Lose");
        }        

        // not neccessary anymore, since we switch to the loosing screen before the next dungeon gets generated
        /* for(int i = 0; i < dungeonRooms.Length; i++)
        {
            Destroy(dungeonRooms[i]);
        }

        dungeonGenerator.SetActive(true);
        dungeonGenerator.GetComponent<DungeonGenerator>().CreateNewOne(); */
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
            GameStatusChange();
            StartCoroutine(NewRoom());

            // This if-statement can be used for activating the boss, once the player arrives in the last room
            if(currentRoom == Game.lastRoom)
            {                
            //     for(int i = 0; i < currentRoom.transform.childCount; i++) 
            //     {
            //         currentRoom.transform.GetChild(i).gameObject.SetActive(true);
            //     } 
            }
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
        GameStatusChange();
    }
}