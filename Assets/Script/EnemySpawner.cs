using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    //Enemy�̔�������Ԋu�̒萔
    private const float _spawnInterval = 1.0f;
    //���������邽�߂̃^�C�}�[�̏����l
    private const float _spawnTimer = 0.0f;
    //�����_���ɔ�������ꏊ��x���W�̍ŏ��l
    private const float _spawmRandomMin = -25.0f;
    //�����_���ɔ�������ꏊ��x���W�̍ő�l
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
