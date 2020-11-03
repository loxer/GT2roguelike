using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    // simple follow AI
    // https://www.youtube.com/watch?v=rhoQd6IAtDo

    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] private float howClose = 3f;

    private Transform target; // the Object the Enemy is chasing after
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) > howClose)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.fixedDeltaTime);
        }
    }
}
