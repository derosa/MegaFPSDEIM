using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimueloProjectile : Projectile {

    public float shotForce = 5f;
    public float shotAngle = 45f;

    public override void Activate()
    {
        base.Activate();
        Rigidbody body = GetComponent<Rigidbody>();
        body.isKinematic = false;
        Vector3 shotDirection = new Vector3(0f, shotAngle / 90f, 1f);
        body.AddRelativeForce(shotDirection * shotForce, ForceMode.VelocityChange);
    }
}
