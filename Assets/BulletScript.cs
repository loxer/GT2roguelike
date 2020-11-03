﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    //https://www.youtube.com/watch?v=wkKsl1Mfp5M&t=711s
    [SerializeField] private int damage;
    [SerializeField] private GameObject impactEffect; 

    private void OnTriggerEnter2D (Collider2D other)
    {
        EnemyDamage enemy = other.GetComponent<EnemyDamage>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        GameObject instantiatedImpact = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(instantiatedImpact, 1f);
        Destroy(gameObject); //destroy that bullet
    }
}
