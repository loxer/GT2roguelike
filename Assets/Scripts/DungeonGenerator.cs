using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.IO;
using System.ComponentModel;
using System;

public class DungeonGenerator : MonoBehaviour
{

    private const float TIME_TO_CHECK_AGAIN = 0.25f;

    [SerializeField] private GameObject roomFolder = default;
    [SerializeField] private GameObject startPoints = default;

    public Transform[] startPos;
    public GameObject[] dungeons;       // available room types for generating the map
    private GameObject[] dungeonRooms;   // used rooms for the current map    
    public LayerMask dugeonMask;
    private NonPathDungeon nonPathDungeon;
    private GameCoordinator gameCoordinator;
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
    private bool finished = false;

    CameraControl cam;



    public void CreateRoom(int start, int end, bool random, Vector3 position)
    {
        int dugeonType = start;
        if (random)
        {
            dugeonType = UnityEngine.Random.Range(start, end);
        }
        Instantiate(dungeons[dugeonType], position, Quaternion.identity, roomFolder.transform);
    }


    private void Start()
    {
        int r = UnityEngine.Random.Range(0, startPos.Length);
        transform.position = startPos[r].position;        
        CreateRoom(0, dungeons.Length, true, transform.position);
        direction = UnityEngine.Random.Range(1, 6);
        nonPathDungeon = GetComponent<NonPathDungeon>();

        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraControl>();
        cam.GoToNextDungeonRoom(transform.position);

        dungeonRooms = new GameObject[startPoints.transform.childCount];
        gameCoordinator = this.transform.GetComponentInParent<GameCoordinator>();
        
        StartCoroutine(CheckForFinishedDungeonProcess());             

    }


    private IEnumerator CheckForFinishedDungeonProcess()
    {
        while(!finished)
        {
            yield return new WaitForSeconds(TIME_TO_CHECK_AGAIN);            

            if(stop && DungeonGeneratorHasFinished() && !finished)
            {
                if(roomFolder.transform.childCount >= startPoints.transform.childCount)
                {
                    bool noEmptyRooms = true;
                    for(int i = 0; i < roomFolder.transform.childCount; i++)
                    {
                        GameObject currentRoom = roomFolder.transform.GetChild(i).gameObject;
                        if(currentRoom.transform.childCount == 0)
                        {
                            Destroy(currentRoom);
                            noEmptyRooms = false;
                        }
                    }

                    if(noEmptyRooms && roomFolder.transform.childCount == startPoints.transform.childCount)
                    {
                        for(int i = 0; i < dungeonRooms.Length; i++)
                        {
                            dungeonRooms[i] = roomFolder.transform.GetChild(i).gameObject;
                        }
                        
                        finished = true;
                        gameCoordinator.DungeonGenerationFinished(dungeonRooms, this.gameObject);                        
                    }
                }
            }
        }
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

                CreateRoom(0, dungeons.Length, true, transform.position);

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


                CreateRoom(2, 4, true, transform.position);

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
                        CreateRoom(3, 0, false, transform.position);
                    } 
                    else
                    {
                        collider.GetComponent<Dungeon>().selfDestruct();
                        int dugeonType2 = UnityEngine.Random.Range(1, 4);
                        if( dugeonType2 == 2)
                        {
                            dugeonType2 = 1;
                        }
                        CreateRoom(dugeonType2, 0, false, transform.position);
                    }
                } 
                transform.position = new Vector2(transform.position.x, transform.position.y - moveTransform);

                CreateRoom(2, 4, true, transform.position);

                direction = UnityEngine.Random.Range(1, 6);
                    
            }
            else
            {   
                // end of generation process
                stop = true;
            }
        }
    }

    public bool DungeonGeneratorHasFinished()
    {        
        if(nonPathDungeon.HasStopped())
        {
            return true;
        }
        else
        {
            return false;
        }
    }    
}