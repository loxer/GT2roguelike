using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{    
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float goToRoomSpeed = 2f;

    private const float ZOOM_IN_WAITING_TIME = 1f;
    private const float ORTHOGRAPHIC_SIZE_FOR_ROOM = 5.0f;

    private float zoomOutOrthographicSize;
    private Vector3 zoomOutPosition;
    private bool cameraInPosition;

    void Start()
    {
        zoomOutPosition = transform.position;
        zoomOutOrthographicSize = Camera.main.orthographicSize;
    }

    public void ZoomIn(Vector3 roomPosition)
    {
        StartCoroutine(Zoom(roomPosition, true));
    }

    public void ZoomOut()
    {
        StartCoroutine(Zoom(zoomOutPosition, false));
    }

    private IEnumerator Zoom(Vector3 position, bool zoomIn)
    {
        cameraInPosition = false;
        yield return new WaitForSeconds(ZOOM_IN_WAITING_TIME);
        
        while(!cameraInPosition)
        {
            // just update as often as possible to make the zoom smooth
            yield return new WaitForSeconds(0.0001f); 

            // translate the cam to the first room
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * zoomSpeed);

            // change the orthographicSize, which makes the zoom effect
            if(zoomIn)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize - (Time.deltaTime * zoomSpeed * 0.65f);
            }
            else
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize + (Time.deltaTime * zoomSpeed * 0.65f);
            }
            
            
            if(zoomIn)
            {
                // make sure it's not zooming too much
                if(Camera.main.orthographicSize < ORTHOGRAPHIC_SIZE_FOR_ROOM)
                {
                    Camera.main.orthographicSize = ORTHOGRAPHIC_SIZE_FOR_ROOM;
                }
            }
            else
            {
                if(Camera.main.orthographicSize > zoomOutOrthographicSize)
                {
                    Camera.main.orthographicSize = zoomOutOrthographicSize;
                }
            }            

            // check if the camera arrived all positions
            if(zoomIn) 
            {
                if(transform.position == position && Camera.main.orthographicSize == ORTHOGRAPHIC_SIZE_FOR_ROOM)
                {
                    cameraInPosition = true;
                }
            }
            else
            {
                if(transform.position == position && Camera.main.orthographicSize == zoomOutOrthographicSize)
                {
                    cameraInPosition = true;
                }
            }
        }
    }

    public IEnumerator GoToRoom(Vector3 roomPosition)
    {        
        cameraInPosition = false;
        
        while(!cameraInPosition)
        {
            // just update as often as possible to make the transition smooth
            yield return new WaitForSeconds(0.0001f); 

            // translate the cam to the first room
            transform.position = Vector3.MoveTowards(transform.position, roomPosition, Time.deltaTime * goToRoomSpeed);
            
            // check if the camera arrived all positions
            if(transform.position == roomPosition)
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
