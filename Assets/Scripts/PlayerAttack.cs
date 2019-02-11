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
    GameObject nextProjectile;


    private void Awake()
    {
        mybody = GetComponent<Rigidbody>();
        CreateNewProjectile();
    }

    private void CreateNewProjectile()
    {
        nextProjectile = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        nextProjectile.transform.SetParent(bulletSpawnPoint);
        nextProjectile.transform.localPosition = Vector2.zero;
        nextProjectile.transform.forward = Camera.main.transform.forward * 100f;
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
            if (nextProjectile == null)
            {
                CreateNewProjectile();
            }

            Ray ray = Camera.main.ViewportPointToRay(Vector2.one * 0.5f);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, bulletMask))
            {
                nextProjectile.transform.LookAt(hit.point);
            }
            else
            {
                nextProjectile.transform.LookAt(Camera.main.transform.forward * 1000f);
            }
            nextProjectile.transform.SetParent(null, true);

            var bulletComponent = (IBullet)nextProjectile.GetComponent(typeof(IBullet));
            bulletComponent.Fire();

            nextShotTime = Time.time + shotDelay;
            Destroy(nextProjectile, bulletTTL);

            CreateNewProjectile();
        }
    }


}
