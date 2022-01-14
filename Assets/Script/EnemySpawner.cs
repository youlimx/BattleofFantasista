using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    //Enemyの発生する間隔の定数
    private const float _spawnInterval = 1.0f;
    //発生させるためのタイマーの初期値
    private const float _spawnTimer = 0.0f;
    //ランダムに発生する場所のx座標の最小値
    private const float _spawmRandomMin = -25.0f;
    //ランダムに発生する場所のx座標の最大値
    private const float _spawnRandomMax = 25.0f;

    void Update()
    {

        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnInterval)
        {
            Instantiate(enemy, this.transform.position + this.transform.right * Random.Range(_spawnRandomMin, _spawnRandomMax), this.transform.rotation);
            _spawnTimer = 0.0f;
        }

    }
}
