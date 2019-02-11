using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

interface IExplodable
{
    void Explode(float force, Vector3 position, float radius, float upwardModifier);
}


public class Balbasu : MonoBehaviour, IExplodable
{
    enum Status
    {
        None,
        Falling,
        Following,
        Dead
    }

    Status status = Status.None;

    NavMeshAgent agent;
    Transform target;
    Rigidbody body;
    RandomAudio randomAudio;

    public void Explode(float force, Vector3 position, float radius, float upwardModifier)
    {
        SwitchStatus(Status.Dead);
        body.AddExplosionForce(force, position, radius, upwardModifier);
        randomAudio.PlayRandomAudio();
        Destroy(gameObject, UnityEngine.Random.Range(3f, 5f));
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
        randomAudio = GetComponent<RandomAudio>();
        SwitchStatus(Status.Falling);
    }

    void Update()
    {
        switch (status)
        {
            case Status.Following:

                if (agent != null && target != null)
                {
                    agent.SetDestination(target.position);
                }
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (status)
        {
            case Status.Falling:
                if (collision.collider.CompareTag("Floor"))
                {
                    SwitchStatus(Status.Following);
                }
                break;
            default:
                break;
        }
    }

    private void SwitchStatus(Status newStatus)
    {
        if (newStatus == status) return;
        status = newStatus;
        //Debug.Log("New Status: " + status);
        switch (status)
        {
            case Status.Falling:
                agent.enabled = false;
                body.isKinematic = false;
                break;
            case Status.Following:
                target = GameObject.FindGameObjectWithTag("Player").transform;
                agent.enabled = true;
                body.isKinematic = true;
                break;
            case Status.Dead:
                body.isKinematic = false;
                agent.enabled = false;
                target = null;
                break;
            default:
                break;
        }
    }
}
