using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    // simple follow AI
    // https://www.youtube.com/watch?v=rhoQd6IAtDo

    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] private float howClose = 3f;
    [SerializeField] private  Rigidbody2D rb;

    private Transform target; // the Object the Enemy is chasing after
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) > howClose)
            {
                rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.fixedDeltaTime));
            }
        }
    }
}
