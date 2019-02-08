using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shotDelay = 0.5f;
    public float bulletTTL = 5f;
    public LayerMask bulletMask;

    Rigidbody mybody;
    float nextShotTime = 0f;

    private void Awake()
    {
        mybody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            Ray ray = Camera.main.ViewportPointToRay(Vector2.one * 0.5f);
            GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, bulletMask))
            {
                newBullet.transform.LookAt(hit.point);
            }
            else
            {
                newBullet.transform.LookAt(Camera.main.transform.forward * 1000f);
            }

            nextShotTime = Time.time + shotDelay;
            Destroy(newBullet, bulletTTL);
        }
    }


}
