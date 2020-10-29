﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPathDungeon : MonoBehaviour
{
    public LayerMask dugeonMask;
    public DungeonGenerator dungeonGenerator;

    private bool stop = false;

    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 1, dugeonMask);
        if (collider == null && dungeonGenerator.stop == true)
        {
            int r = UnityEngine.Random.Range(0, 4);
            dungeonGenerator.InstantiateDungeonRoom(dungeonGenerator.dungeons[r], transform.position);
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
