using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    // https://www.youtube.com/watch?v=7-8nE9_FwWs
    private Vector3 target;
    [SerializeField] private GameObject player = default;
    [SerializeField] private GameObject bulletPrefab = default;
    [SerializeField] private float bulletSpeed = 60;
    [SerializeField] private GameObject bulletStart = default; 
    void Update()
    {
        // as long as the player is still alive
        if (!player.Equals((null)))
        {
            target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, Input.mousePosition.z));
            Vector3 difference = target - player.transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            if (Input.GetMouseButtonDown(0))
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                FireBullet(direction, rotationZ);
            }
        }
    }

    void FireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        Destroy(b, 3f); // destroy bullet 3 seconds after instantiation
    }
}
