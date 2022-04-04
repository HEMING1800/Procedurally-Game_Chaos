using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Each time the player kills the enemy, the score boeard should be updated
*/
public class ScoreBoard : MonoBehaviour
{   
    public static int score {get; private set; }

    // Start is called before the first frame update
    void Start()
    {   
        // Link the enemy death event with score board updating
        Enemy.OnDeathStatic += OnEnemyKilled; 

        // Link the player death event with score board updating
        FindObjectOfType<Player>().OnDeath += OnPlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add score when kill the enemy
    void OnEnemyKilled(){
        score += 10;
    }

    // Unlink the enemy death event with score board updating
    void OnPlayerDeath(){
        score = 0;
        Enemy.OnDeathStatic -= OnEnemyKilled;
    }

}
