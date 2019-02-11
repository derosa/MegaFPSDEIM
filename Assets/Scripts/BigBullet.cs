using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    void Fire();
}

public class BigBullet : MonoBehaviour, IBullet
{

    public AnimationCurve speedCurve;
    public float timeToMaxSpeed = 1.0f;
    public float maxSpeed = 20f;
    public float boomForce = 500;
    public float explosionRadius = 10f;
    public float upwardModifier = 10;
    public GameObject explosionPrefab;
    public Transform particlesTransform;
    TrailRenderer trailRenderer;
    ParticleSystem particles;

    float currentTime = 0f;
    Rigidbody myRB;

    private void Awake()
    {
        //myRB = GetComponent<Rigidbody>();

        trailRenderer = GetComponentInChildren<TrailRenderer>();
        trailRenderer.enabled = false;
        trailRenderer.Clear();

        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();

        enabled = false;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        currentTime = Mathf.Clamp(currentTime, 0f, timeToMaxSpeed);
        myRB.velocity = myRB.transform.forward * speedCurve.Evaluate(currentTime / timeToMaxSpeed) * maxSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        particlesTransform.SetParent(null, true);
        particlesTransform.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);

        var overlappedObjects = Physics.OverlapSphere(collision.contacts[0].point, explosionRadius);

        for (int t = 0; t < overlappedObjects.Length; t++)
        {
            //Debug.Log("Explotando " + overlappedObjects[t].name);
            IExplodable explodable = (IExplodable)overlappedObjects[t].GetComponent(typeof(IExplodable));
            if (explodable != null)
            {
                explodable.Explode(boomForce, transform.position, explosionRadius, upwardModifier);
            }
            else
            {
                var rbs = overlappedObjects[t].GetComponent<Rigidbody>();
                if (rbs != null && rbs != myRB && !rbs.CompareTag("Player"))
                {
                    rbs.AddExplosionForce(boomForce, transform.position, explosionRadius, upwardModifier);
                    Destroy(rbs.gameObject, 5f);
                }
            }
        }

        GameObject expl = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        expl.GetComponent<Explosion>().initialScale = explosionRadius;

        Destroy(gameObject);
    }

    public void Fire()
    {
        myRB = gameObject.AddComponent<Rigidbody>();
        myRB.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        trailRenderer.enabled = true;
        enabled = true;
        particles.Play();
    }
}