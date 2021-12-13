using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzlle;
    public Projectile projectile;
    public float msBetweenShots = 100; // the shooting rate
    public float muzlleVelocity = 35;

    float nextShotTime;

    public void Shoot()
    {
        if (Time.time > nextShotTime){
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate (projectile, muzlle.position, muzlle.rotation) as Projectile;
            newProjectile.SetSpeed(muzlleVelocity); 
        }
    }
}
 