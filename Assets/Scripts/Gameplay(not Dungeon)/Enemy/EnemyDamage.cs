using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //https://www.youtube.com/watch?v=wkKsl1Mfp5M&t=711s
    [SerializeField] private int health = 100;
    public void TakeDamage(int damage) 
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
