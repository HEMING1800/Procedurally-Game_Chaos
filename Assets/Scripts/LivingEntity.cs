using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{   
    public float initialHealth;
    public float health { get; protected set; }
    protected bool dead;

    public event System.Action OnDeath;

    protected virtual void Start()
    {   
        // Implement the initial health level
        health = initialHealth;
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {   
        
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

    // If enetity is dead, it should be destried in the game
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
