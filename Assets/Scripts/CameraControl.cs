using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{    
    public void GoToNextDungeonRoom(Vector3 roomPosition)
    {       
        transform.position = roomPosition;        
    }
}
