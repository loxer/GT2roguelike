using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    // Start is called before the first frame update mostly copied from spawnPoint explaination video on youtube
    public GameObject[] objects;
    private void Start()

    {
        int r = UnityEngine.Random.Range(0, objects.Length);
        GameObject instance = (GameObject) Instantiate(objects[r], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
