﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    //https://www.youtube.com/watch?v=wkKsl1Mfp5M&t=711s
    [SerializeField] private int damage = default;
    [SerializeField] private GameObject impactEffect = default; 

    private void OnTriggerEnter2D (Collider2D other)
    {
        if(!other.CompareTag("Player"))
        {
            EnemyDamage enemy = other.GetComponent<EnemyDamage>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            GameObject instantiatedImpact = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(instantiatedImpact, 0.5f);
            Destroy(gameObject); //destroy that bullet
        }
    }
}
