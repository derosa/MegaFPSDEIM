using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    [Header("Projectile settings")]
    public float cooldownTime = 0.2f;

    protected GameObject projectile;

    protected float nextShootTime = 0f;

    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    public virtual void Release()
    {
        Debug.Log("Releasing " + name);
        gameObject.SetActive(false);
    }

    public virtual void Equip(Transform attachPoint = null)
    {
        Debug.Log("Equipping " + name);
        gameObject.SetActive(true);
        transform.SetParent(attachPoint);
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        if (attachPoint != null)
        {
            transform.rotation = attachPoint.rotation;
        }
    }

    public virtual void Activate()
    {
        if (Time.time > nextShootTime)
        {
            Debug.Log("Bang!");
            nextShootTime = Time.time + cooldownTime;
            if (projectile == null)
            {
                projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectileSpawnPoint != null ? projectileSpawnPoint : transform);
            }

            LookForward(projectile.transform);

            projectile.GetComponent<Projectile>().Activate();
            projectile = null;
        }
    }

    protected void LookForward(Transform t)
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector2.one * 0.5f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            t.LookAt(hit.point);
        }
        else
        {
            t.LookAt(Camera.main.transform.forward * 1000f);
        }
    }
}
