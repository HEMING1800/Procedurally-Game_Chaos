using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class controls the enemy which generated on the level
*/
[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State{Idle, Chasing, Attacking}; // Enemy can be idle, chasing, or attacking
    State currentState;

    public ParticleSystem deathEffect; // The partical effect once the entity is dead
    public static event System.Action OnDeathStatic;

    UnityEngine.AI.NavMeshAgent pathFinder;
    Transform target; // Trace the player
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

    // Use Awake to initialize variables or states before the application starts
    void Awake(){
        
        GameObject playerFind = GameObject.FindGameObjectWithTag("Player");
        pathFinder = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if( playerFind != null)
        {   
            hasTarget = true;
            
            target = playerFind.transform; // Trace the game obejct with Tag "Player"  
            targetEntity = target.GetComponent<LivingEntity>();

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        }
        
    }

    protected override void Start()
    {
        // Call the Start on the LivingEntity
        base.Start();

        if(hasTarget)
        {   
            currentState = State.Chasing;        
            targetEntity.OnDeath += OnTargetDeath;

            StartCoroutine (UpdatePath ());
        }
    }

    public void SetCharacteristics(float moveSpeed, int hitsToKillPlayer, float enemyHealth, Color skinColor)
    {
        pathFinder.speed = moveSpeed;

        if (hasTarget){
            damage = Mathf.Ceil(targetEntity.initialHealth / hitsToKillPlayer);
        }
        initialHealth = enemyHealth;

        // Set the enemy's color
        skinMaterial = GetComponent<Renderer>().material;
        skinMaterial.color = skinColor;
        originalColor = skinMaterial.color;
    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {   
        // If the enemy is dead, create the death effect
        if(damage >= health){
            if(OnDeathStatic != null){
                OnDeathStatic();
            }

            // Destroy the enemy death effect particles after start life time
           Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }

    // Once the target died, no longer has target
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
