using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public enum gates
    {
        LR = 0,
        LBR = 1,
        LTR = 2,
        LTRB = 3
    };

    public gates open;


    public void selfDestruct()
    {
        Destroy(gameObject);
    }
}
