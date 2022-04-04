using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    This class control the gun system in the game
    Such as which position the gun shoud be spawn
*/
public class GunController : MonoBehaviour
{   
    public Transform weaponHold; 
    public Gun startingGun;

    Gun equippedGun;

    void Start()
    {
        if(startingGun != null)
        {
            EquipGun(startingGun);
        }
    }
    public void EquipGun(Gun gunToEquip)
    {
        if(equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
       
        equippedGun = Instantiate (gunToEquip, weaponHold.position, weaponHold.rotation) ;
        equippedGun.transform.parent = weaponHold; 
    }

    public void Shoot()
    {
        if(equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }

    // Get the height of the weapon
    public float GunHeight
    {
        get{
            return weaponHold.position.y;
        }
    }
}
