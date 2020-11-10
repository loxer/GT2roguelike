using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    private Camera camera;

    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    
    public void DungeonGenerationFinished(GameObject[] dungeonRooms, GameObject dungeonGenerator)
    {
        dungeonGenerator.gameObject.SetActive(false);        

        for(int i = 0; i < dungeonRooms.Length; i++)
        {
            if(i > 0)
            {
                dungeonRooms[i].SetActive(false);
            }
            else
            {
                // camera.orthographicSize = 5.0f;
                camera.GoToNextDungeonRoom(dungeonRooms[i].transform.position); // Start room
            }
        }
    }
}
