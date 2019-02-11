using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawnPrefab : MonoBehaviour
{
    public GameObject prefab;
    public float timeInterval = 1f;
    public float randomRange = 0.1f;

    float nextSpawnTime = 0f;
    private void Start()
    {
        nextSpawnTime = Time.time + timeInterval;
    }

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + timeInterval;
            Vector3 offset = UnityEngine.Random.onUnitSphere * randomRange;
            GameObject crate = Instantiate(prefab, transform.position + offset, Quaternion.identity);
        }
    }
}
