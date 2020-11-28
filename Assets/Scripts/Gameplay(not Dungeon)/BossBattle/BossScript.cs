﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public int health = 200;
    public int damage;
    private float timeBtwDamage = 1.5f;


    //public Animator camAnim;
    private Slider healthBar;
    private Animator anim;
    public bool isDead;

    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("BossBar").GetComponent<Slider>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 50) {
            anim.SetTrigger("stageTwo");
        }

        if (health <= 0) {
            anim.SetTrigger("death");
        }

        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0) {
            timeBtwDamage -= Time.deltaTime;
        }

        healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
        if (other.CompareTag("Player") && isDead == false) {
            if (timeBtwDamage <= 0) {
              //  camAnim.SetTrigger("shake");
                other.GetComponent<DisCharge>().battery.value -= damage;
            }
        } 
    }
}
