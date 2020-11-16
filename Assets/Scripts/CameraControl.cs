using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{    
    [SerializeField] private float zoomInSpeed = 2f;

    private const float TRANSLATE_TO_NEXT_ROOM_SPEED = 2f;    
    private const float ZOOM_IN_WAITING_TIME = 1f;
    private const float ORTHOGRAPHIC_SIZE_FOR_ROOM = 5.0f;

    private bool cameraInPosition;

    public IEnumerator StartingZoom(Vector3 roomPosition)
    {
        yield return new WaitForSeconds(ZOOM_IN_WAITING_TIME);
        cameraInPosition = false;
        
        while(!cameraInPosition)
        {
            // just update as often as possible to make the zoom smooth
            yield return new WaitForSeconds(0.0001f); 

            // translate the cam to the first room
            transform.position = Vector3.MoveTowards(transform.position, roomPosition, Time.deltaTime * zoomInSpeed);

            // reduce the orthographicSize, which makes the zoom-in effect
            Camera.main.orthographicSize = Camera.main.orthographicSize - (Time.deltaTime * zoomInSpeed * 0.65f);
            
            // make sure it's not zooming too much
            if(Camera.main.orthographicSize < ORTHOGRAPHIC_SIZE_FOR_ROOM)
            {
                Camera.main.orthographicSize = ORTHOGRAPHIC_SIZE_FOR_ROOM;
            }

            // check if the camera arrived all positions
            if(transform.position == roomPosition && Camera.main.orthographicSize == ORTHOGRAPHIC_SIZE_FOR_ROOM)
            {
                cameraInPosition = true;
            }
        }
    }

    public bool IsInPosition()
    {
        return cameraInPosition;
    }
}
