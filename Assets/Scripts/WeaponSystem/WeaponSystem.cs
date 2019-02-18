using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{

    public Transform attachPoint;
    public List<GameObject> weaponPrefabs;
    public float weaponChangeDelay = 0.2f;

    private Weapon currentWeapon = null;
    private List<Weapon> weapons = new List<Weapon>();
    private int currentWeaponIndex = 0;
    private float nextWeaponChangeTime = 0f;

    private void Awake()
    {
        for (int t = 0; t < weaponPrefabs.Count; t++)
        {
            var newWeapon = Instantiate(weaponPrefabs[t]).GetComponent<Weapon>();
            Debug.Log("Loading " + newWeapon.name);
            weapons.Add(newWeapon);
        }
    }

    public void ChangeWeapon(int offset)
    {
        if (Time.time < nextWeaponChangeTime) return;
        nextWeaponChangeTime = Time.time + weaponChangeDelay;

        if (currentWeapon != null)
        {
            currentWeapon.Release();
        }

        currentWeaponIndex += offset;

        if (currentWeaponIndex >= weapons.Count)
        {
            currentWeaponIndex = 0;
        }
        else if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Count - 1;
        }

        currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.Equip(attachPoint);
    }

    internal void Activate()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Activate();
        }
    }
}
