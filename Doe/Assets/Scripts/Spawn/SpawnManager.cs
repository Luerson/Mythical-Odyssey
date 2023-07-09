using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] _spawnPoints;
    private Transform _playerPosition;
    public GameObject[] enemy;
    public float InitialSpawnTime;
    public float IntervalSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnEnemies", InitialSpawnTime, IntervalSpawnTime);
    }

    void SpawnEnemies()
    {
        transform.position = _playerPosition.position;

        int spawnIndex = Random.Range(0, _spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemy.Length);

        Instantiate(enemy[enemyIndex], _spawnPoints[spawnIndex].position, Quaternion.identity);
    }
}