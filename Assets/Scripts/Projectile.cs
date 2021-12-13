using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask; // determine wchich layer that the projectile colides with
    float speed = 10;
    float damage = 1;

    float lifetime = 3;

    // avoid the collision detaction disable when the projecticle is in enemy's body and not start detact collision
    // increase enemy's speed should increase skinWidth variable
    float skinWidth = .1f; 

    void Start()
    {   
        // destroy the bullet after certain time
        Destroy(gameObject, lifetime);

        // get the all colliders which prohectile can intersecting with
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if(initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);

        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage, hit);
        }
        GameObject.Destroy(gameObject);
    }

    void OnHitObject(Collider colliders)
    {   
        IDamageable damageableObject = colliders.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
        }
        GameObject.Destroy(gameObject);

    }

    // set the projectile spped
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }


}
