/*
Spawner charges the enemies display on the map
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{   
    public Wave[] waves;
    public Enemy enemy; 

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    void Start()
    {
        NextWave();
    }

    void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawn;

            Enemy spawnedEnemy = Instantiate (enemy, Vector3.zero, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath; // OnEnemyDeath subscribes to OnDeath event
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive --;

        if(enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }
    void NextWave()
    {   
        currentWaveNumber ++;

        if(currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber-1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }

    // Enemy attacks wave
    [System.Serializable]
   public class Wave
   {
       public int enemyCount;
       public float timeBetweenSpawn;

   }
}
