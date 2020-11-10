using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //https://www.youtube.com/watch?v=wkKsl1Mfp5M&t=711s
    [SerializeField] private int health = 90;
    //[SerializeField] private GameObject deathEffect;
    //TODO public ist nicht so schön
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
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
