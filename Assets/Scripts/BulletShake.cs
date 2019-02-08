using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShake : MonoBehaviour
{
    public float amount = 0.05f;

    void Update()
    {
        transform.localPosition = UnityEngine.Random.insideUnitSphere * amount;
    }
}
