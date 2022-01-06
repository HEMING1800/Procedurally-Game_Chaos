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
    
    LivingEntity playerEntity;
    Transform playerT;

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;
    float timeBetweenCampingChecks = 2;
    float campThresholdDistance = 1.5f;
    float nextCampCheckTime;
    bool isCamping;
    Vector3 campPositionOld;

    bool isDisable; // when the player dead, True

    MapGenerator map;

    public event System.Action<int> OnNewWave;

    void Start()
    {   
        // find the player position
        playerEntity = FindObjectOfType<Player>();
        playerT = playerEntity.transform;

        nextCampCheckTime = timeBetweenCampingChecks + Time.time;
        campPositionOld = playerT.position;
        playerEntity.OnDeath += OnPlayerDeath;

        map = FindObjectOfType<MapGenerator>();
        NextWave();
    }

    void Update()
    {   
        if(!isDisable)
        {
            if(Time.time > nextCampCheckTime)
            {
                nextCampCheckTime = Time.time + timeBetweenCampingChecks;

                isCamping = (Vector3.Distance(playerT.position, campPositionOld) < campThresholdDistance);
                campPositionOld = playerT.position;
            }

            if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
            {
                enemiesRemainingToSpawn--;
                nextSpawnTime = Time.time + currentWave.timeBetweenSpawn;

                StartCoroutine(SpawnEnemy());
            }
        }
    }
    
    // Initiate enemies on the map
    IEnumerator SpawnEnemy()
    {   
        float spawnDelay = 1;
        float tileFlashSpeed = 4; 
        Transform spawnTile = map.GetRandomOpenTile(); //get random map position 

        if(isCamping)
        {
            spawnTile = map.GetTileFromPosition(playerT.position);
        }

        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColor = tileMat.color;
        Color flashColor = Color.red;
        float spawnTimer = 0;

        // flash the tile before enemy is generated
        while(spawnTimer < spawnDelay)
        {   
            tileMat.color = Color.Lerp(initialColor, flashColor, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));

            spawnTimer += Time.deltaTime;
            yield return null;
        }

        Enemy spawnedEnemy = Instantiate (enemy, spawnTile.position + Vector3.up, Quaternion.identity) as Enemy;
        spawnedEnemy.OnDeath += OnEnemyDeath; // OnEnemyDeath subscribes to OnDeath event
    }

    void OnPlayerDeath()
    {
        isDisable = true;
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

            if(OnNewWave != null)
            {
                OnNewWave(currentWaveNumber);
            }
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
