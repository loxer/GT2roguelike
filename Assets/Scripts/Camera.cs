using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{    
    public void GoToNextDungeonRoom(Vector3 roomPosition)
    {       
        transform.position = roomPosition;        
    }
}
