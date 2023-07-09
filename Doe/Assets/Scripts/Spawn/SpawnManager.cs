using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] _spawnPoints;
    private Transform _playerPosition;
    public GameObject[] enemy;

    // Start is called before the first frame update
    void Start()
    {
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnEnemies", 0.5f, 0.5f);
    }

    void SpawnEnemies()
    {
        transform.position = _playerPosition.position;

        int spawnIndex = Random.Range(0, _spawnPoints.Length+1);
        int enemyIndex = Random.Range(0, enemy.Length+1);

        Instantiate(enemy[enemyIndex], _spawnPoints[spawnIndex].position, Quaternion.identity);
    }
}