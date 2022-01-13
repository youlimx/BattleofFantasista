using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    private const int SpawnInterval = 1.0f;
    private float _spawnTimer = 0.0f;
    private float _spawmRandomMin = -25.0f;
    private float _spawnRandomMax = 25.0f;

    void Update()
    {

        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= SpawnInterval)
        {
            Instantiate(enemy, this.transform.position + this.transform.right * Random.Range(_spawnRandomMin, _spawnRandomMax), this.transform.rotation);
            _spawnTimer = 0.0f;
        }

    }
}
