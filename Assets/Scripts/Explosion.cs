using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector]
    public float initialScale = 2.5f;
    public float timeToExtint = 0.1f;

    private void Awake()
    {
        transform.localScale = Vector3.one * initialScale;
        if (CameraShaker.Instance != null)
        {
            CameraShaker.Instance.Shake(initialScale * 0.25f, timeToExtint * 2f);
        }
    }

    void Update()
    {
        initialScale -= Time.deltaTime * 1f / timeToExtint;
        if (initialScale <= 0f)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localScale = Vector3.one * initialScale;
        }


    }
}
