using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance { get; private set; }
    public float defaultShakeAmount = 1.0f;
    public float defaultShakeTime = 1.0f;

    bool isShaking = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Shake();
        }
    }

    public void Shake()
    {
        Shake(defaultShakeAmount, defaultShakeTime);
    }

    public void Shake(float scale, float time)
    {
        if (isShaking) return;
        StartCoroutine(ShakeCoroutine(scale, time));
    }

    IEnumerator ShakeCoroutine(float scale, float time)
    {
        isShaking = true;
        Vector3 originalPosition = transform.localPosition;

        while (time > 0f)
        {
            Vector3 offset = UnityEngine.Random.insideUnitCircle * scale;
            transform.localPosition = originalPosition + offset;
            time -= Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        isShaking = false;
    }
}
