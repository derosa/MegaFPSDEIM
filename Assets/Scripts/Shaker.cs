using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public float shakeAmount = 0.1f;
    public bool useRotation;

    Quaternion originalRotation;
    Vector3 originalPosition;
    private void Awake()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        if (useRotation)
        {
            ShakeRotation();
        }
        else
        {
            ShakePosition();
        }
    }

    private void ShakePosition()
    {
        transform.localPosition = originalPosition + UnityEngine.Random.onUnitSphere * shakeAmount;
    }

    private void ShakeRotation()
    {
        transform.rotation = originalRotation *
            Quaternion.Euler(UnityEngine.Random.onUnitSphere * shakeAmount);
    }
}
