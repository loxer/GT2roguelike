
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPathDungeon : MonoBehaviour
{
    //private LayerMask dugeonMask;
    //private DungeonGenerator dungeonGenerator;

    public LayerMask dugeonMask;
    public DungeonGenerator dungeonGenerator;
    private bool stop = false;

    
 /*  void Start()
   {
       dugeonMask = LayerMask.GetMask("Dungeon");
       dungeonGenerator = GameObject.FindWithTag("DungeonGenerator").GetComponent<DungeonGenerator>();
   }*/
   
   void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 1, dugeonMask);
        if (collider == null && dungeonGenerator.stop == true)
        {            
            dungeonGenerator.CreateRoom(0, 4, true, transform.position);
        }
        else
        {
            stop = true;
        }
    }

    public bool HasStopped()
    {
        return stop;
    }
}
