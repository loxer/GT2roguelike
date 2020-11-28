using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    // Start is called before the first frame update mostly copied from spawnPoint explaination video on youtube
    public GameObject[] enemy;

    void Start()
    {
            int r = UnityEngine.Random.Range(0, enemy.Length);
            GameObject instance = (GameObject)Instantiate(enemy[r], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
     }
}


