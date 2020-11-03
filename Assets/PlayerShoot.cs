using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //Tutorial by Alexander Zotov https://www.youtube.com/watch?v=01HVr1fp7pU
    [SerializeField] private GameObject bullet = default;
    [SerializeField] private Transform firePoint = default;
    [SerializeField] private float bulletSpeed = 10f; 
    
    private Vector2 lookDirection;
    private float lookAngle;
    private Camera cam;
    
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        lookDirection = cam.ScreenToWorldPoint(Input.mousePosition);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle); // rotates firePoint towards mouse cursor

        if (Input.GetMouseButtonDown(0))
        {
            FireBullet(); 
        } 
    }

    void FireBullet()
    {
        GameObject firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        firedBullet.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed; 
        Destroy(firedBullet, 3f); // destroy bullet 3 seconds after instantiation
    }
}
