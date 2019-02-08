using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShakeOnCollision : MonoBehaviour {

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Ouch");
        if (col.transform.CompareTag("Block"))
        {
            CameraShaker.Instance.Shake();
        }
    }
}
