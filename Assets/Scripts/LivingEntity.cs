using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{   
    public float initialHealth;
    protected float health;
    protected bool dead;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = initialHealth;
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {   
        //TO DO: hit
        TakeDamage(damage);
    }

    // Entity takes the damage, and died if the the health level is exhausted
    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    [ContextMenu("Self Destruct")]
    protected void Die()
    {
        dead = true;
        if( OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}
