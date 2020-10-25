﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonPathDungeon : MonoBehaviour
{
    public LayerMask dugeonMask;
    public DungeonGenerator dungeonGenerator;
    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 1, dugeonMask);
        if (collider == null && dungeonGenerator.stop == true)
        {
            int r = UnityEngine.Random.Range(0, 4);
            Instantiate(dungeonGenerator.dungeons[r], transform.position, Quaternion.identity);
        }
    }
}
