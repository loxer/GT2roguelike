using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    private CameraControl cam;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraControl>();
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
                // cam.orthographicSize = 5.0f;
                cam.GoToNextDungeonRoom(dungeonRooms[i].transform.position); // Start room
            }
        }
    }
}
