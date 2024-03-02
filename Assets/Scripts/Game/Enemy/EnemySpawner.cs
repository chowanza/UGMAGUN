using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _minimumSpawnTime;

    [SerializeField]
    private float _maximumSpawnTime;

    private float _timeUntilSpawn;

    public int currentRound = 1;
    public int enemiesRemaining = 0;

    public int enemiesPerRound = 10;


    void Awake(){
        startRound();
        //SetTimeUntilSpawn();
    }

    void startRound(){
        enemiesRemaining = enemiesPerRound;
        spawnEnemies();
    }

    void spawnEnemies(){
        for(int i=0; i<enemiesRemaining; i++){
            _timeUntilSpawn -= Time.deltaTime;

            if (_timeUntilSpawn <= 0){
                Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }
        }
    }

    void Update(){
            if(enemiesRemaining > 0){
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                if (enemies.Length == 0){
                    currentRound ++;
                    enemiesPerRound += 5;
                    startRound();
                }
            }
        }
    
    public void enemyDestroyed(){
        enemiesRemaining --;
    }
    
    void SetTimeUntilSpawn(){
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }

}

    

