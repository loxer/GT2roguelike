﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System.IO;
using System.ComponentModel;
using System;

public class DungeonGenerator : MonoBehaviour
{
    public Transform[] startPos;
    public GameObject[] dungeons;
    public LayerMask dugeonMask;
    public int dungeonLevelDepth;
    public int pathDepth;

    private int direction;
    public float moveTransform;

    private float time;
    public float spawnDelta = 1.0f;

    public float minX;
    public float maxX;
    public float minY;
    public bool stop = false;

    private void Start()
    {
        int r = UnityEngine.Random.Range(0, startPos.Length);
        transform.position = startPos[r].position;
        Instantiate(dungeons[0], transform.position, Quaternion.identity);

        direction = UnityEngine.Random.Range(1, 6);
    }

    private void Update()
    {
        if (time <= 0 && !stop)
        {
            move();
            time = spawnDelta;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    private void move()
    {



        // left process




        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX)
            {
                pathDepth = 0;
                transform.position = new Vector2(transform.position.x + moveTransform, transform.position.y);

                int dugeonType = UnityEngine.Random.Range(0, dungeons.Length);
                Instantiate(dungeons[dugeonType], transform.position, Quaternion.identity);

                direction = UnityEngine.Random.Range(1, 6);

                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }



        // right process




        else if (direction == 3 || direction == 4)
        {   
            if (transform.position.x > minX)
            {
                pathDepth = 0;
                transform.position = new Vector2(transform.position.x - moveTransform, transform.position.y);
                
                int dugeonType = UnityEngine.Random.Range(2, 4);
                Instantiate(dungeons[dugeonType], transform.position, Quaternion.identity);

                direction = UnityEngine.Random.Range(3, 6);

            } 
            else
            {
                direction = 5;
            }
        }



        //downwards process


        else if (direction == 5)
        {
            if (transform.position.y > minY)
            {
                pathDepth++;
                dungeonLevelDepth++;

                Collider2D collider = Physics2D.OverlapCircle(transform.position, 1, dugeonMask);
                if (collider.GetComponent<Dungeon>().open != Dungeon.gates.LBR && collider.GetComponent<Dungeon>().open != Dungeon.gates.LTRB)
                {
                    if (pathDepth >= 2)
                    {
                        collider.GetComponent<Dungeon>().selfDestruct();
                        Instantiate(dungeons[3], transform.position, Quaternion.identity);
                    } else
                    {
                        collider.GetComponent<Dungeon>().selfDestruct();
                        int dugeonType2 = UnityEngine.Random.Range(1, 4);
                        if( dugeonType2 == 2)
                        {
                            dugeonType2 = 1;
                        }
                        Instantiate(dungeons[dugeonType2], transform.position, Quaternion.identity);
                    }
                } 
                transform.position = new Vector2(transform.position.x, transform.position.y - moveTransform);

                int dugeonType = UnityEngine.Random.Range(2, 4);
                Instantiate(dungeons[dugeonType], transform.position, Quaternion.identity);

                direction = UnityEngine.Random.Range(1, 6);
                    
            }
            else
            {   
                // end of generation process 
                stop = true;
            }
        }



        
    }

}