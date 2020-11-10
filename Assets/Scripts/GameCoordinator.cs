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
        StartCoroutine(PrepareCameraPosition(dungeonRooms));        
        
    }

    private IEnumerator PrepareCameraPosition(GameObject[] dungeonRooms)
    {
        StartCoroutine(cam.StartingZoom(dungeonRooms[0].transform.position)); // Start room

        while(!cam.IsInPosition())                      // check regularly if the starting zoom has finished
        {
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);            // give the player a second before something new happens
        
        for(int i = 1; i < dungeonRooms.Length; i++)
        {
            dungeonRooms[i].SetActive(false);           // make all rooms disappear (except the first one)
        }
    }
}
