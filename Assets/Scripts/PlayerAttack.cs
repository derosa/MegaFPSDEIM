using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    WeaponSystem weaponSystem;

    private void Start()
    {
        weaponSystem = GetComponent<WeaponSystem>();
        weaponSystem.ChangeWeapon(0);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            weaponSystem.Activate();
        }
        if (Input.mouseScrollDelta.y > 0f)
        {
            weaponSystem.ChangeWeapon(1);
        }
        else if (Input.mouseScrollDelta.y < 0f)
        {
            weaponSystem.ChangeWeapon(-1);
        }
    }
}
