using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    float timer;

    void Update()
    {

        timer += Time.deltaTime;

        if (timer > 1)
        {
            Instantiate(enemy, this.transform.position + this.transform.right * Random.Range(-25.0f, 25.0f), this.transform.rotation);
            timer = 0;
        }

    }
}
