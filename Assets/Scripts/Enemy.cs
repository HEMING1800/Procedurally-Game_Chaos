using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State{Idle, Chasing, Attacking};
    State currentState;

    UnityEngine.AI.NavMeshAgent pathFinder;
    Transform target; //trace the player
    LivingEntity targetEntity;
    Material skinMaterial;
    
    Color originalColor;

    float attackDistanceThreshold = .5f;
    float timeBetweenAttacks = 1;
    float damage = 1;

    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;
    bool hasTarget;

    protected override void Start()
    {
        // call the Start on the LivingEntity
        base.Start();

        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        GameObject playerFind = GameObject.FindGameObjectWithTag("Player");
        if( playerFind != null)
        {   
            hasTarget = true;
            currentState = State.Chasing;
            pathFinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
            target = playerFind.transform; // trace the game obejct with Tag "Player"
            
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        }
        
        StartCoroutine (UpdatePath ());
    }

    // once the target died, no longer has target
    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {   
        if(hasTarget)
        {
            if(Time.time > nextAttackTime){
                float sqrDisToTarget = (target.position - transform.position).sqrMagnitude;

                if(sqrDisToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius,2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator Attack()
    {   
        //ensure the enemy will not attack and updatepath simultaneous
        currentState = State.Attacking;
        pathFinder.enabled = false;


        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        //Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius); // no touch with player
        Vector3 attackPosition = target.position - dirToTarget * myCollisionRadius; // a bit touch with player

        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.red;
        bool hasAppliedDamage = false;

        while(percent <= 1)
        {   
            if(percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        skinMaterial.color = originalColor;
        
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }

        // Check the next position
    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;  // recalculate 4 times per second

        while (hasTarget){
            if(currentState == State.Chasing){
                // stop the enemy at certain distance away from player
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2); 

                if(!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
