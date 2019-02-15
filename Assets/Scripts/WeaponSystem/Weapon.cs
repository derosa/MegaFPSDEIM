using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public GameObject weaponPrefab;
    protected GameObject weapon;

    public virtual void Release()
    {
        if (weapon != null)
        {
            weapon.SetActive(false);
        }
    }
    public virtual GameObject Equip()
    {

        if (weapon == null)
        {
            weapon = Instantiate(weaponPrefab);
        }
        weapon.SetActive(true);
        return weapon;
    }

    public virtual void Activate() { 
        Debug.Log("Bang!");
    }
}
