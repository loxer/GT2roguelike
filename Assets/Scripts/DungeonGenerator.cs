using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System.IO;
using System.ComponentModel;

public class DungeonGenerator : MonoBehaviour
{
    public Transform[] startPos;
    public GameObject[] rooms;

    private int direction;
    public float moveTransform;

    private float time;
    public float timeBetween = 0.25f;

    public float minX;
    public float maxX;
    public float minY;

    private bool stop = false;

    private void Start()
    {
        int r = UnityEngine.Random.Range(0, startPos.Length);
        transform.position = startPos[r].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = UnityEngine.Random.Range(1, 6);
    }

    private void Update()
    {
        if (time <= 0 && stop == false)
        {
            move();
            time = timeBetween;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    private void move()
    {
        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX)
            {
                transform.position = new Vector2(transform.position.x + moveTransform, transform.position.y);
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        {   
            if (transform.position.x > minX)
            {
                transform.position = new Vector2(transform.position.x - moveTransform, transform.position.y);
            } else
            {
                direction = 5;
            }
        }
        else if (direction == 5)
        {
            if (transform.position.y > minY)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - moveTransform);
            } else
            {   
                // end of generation process 
                stop = true;
            }
        }
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = UnityEngine.Random.Range(1, 6);
    }

}