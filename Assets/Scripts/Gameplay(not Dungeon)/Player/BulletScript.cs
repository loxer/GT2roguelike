using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // https://github.com/BlackthornProd/Boss-Battle-Tutorial/blob/master/BossBattle/Assets/Scripts/Projectile.cs
    //https://www.youtube.com/watch?v=wkKsl1Mfp5M&t=711s
    [SerializeField] private int damage = 30;
    [SerializeField] private GameObject impactEffect = default; 
  
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            EnemyDamage enemy = other.GetComponent<EnemyDamage>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            
            if (other.CompareTag("Boss"))
            {
                other.GetComponent<BossScript>().health -= damage;
            }
            
            GameObject instantiatedImpact = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(instantiatedImpact, 0.5f);
            Destroy(gameObject); //destroy that bullet
        }
    }
}
