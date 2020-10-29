using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject[] dungeons;
    
    public void GoToNextDungeonRoom(/* Vector3 roomPosition,  */GameObject[] dungeons)
    {
        this.dungeons = dungeons;
        // transform.position = roomPosition;
        transform.position = dungeons[0].transform.position;
    }

    // void Update()
    // {
    //     if()
    //     DungeonGeneratorHasFinished
    // }

}
