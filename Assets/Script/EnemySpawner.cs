using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemy;

    //Enemy�̔�������Ԋu�̒萔
    private const float SpawnInterval = 1.0f;
    //���������邽�߂̃^�C�}�[�̏����l
    private  float _spawnTimer = 0.0f;
    //�����_���ɔ�������ꏊ��x���W�̍ŏ��l
    private const float SpawmRandomMin = -25.0f;
    //�����_���ɔ�������ꏊ��x���W�̍ő�l
    private const float SpawnRandomMax = 25.0f;

    void Update()
    {

        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= SpawnInterval)
        {
            Instantiate(_enemy, this.transform.position + this.transform.right * Random.Range(SpawmRandomMin, SpawnRandomMax), this.transform.rotation);
            _spawnTimer = 0.0f;
        }

    }
}
