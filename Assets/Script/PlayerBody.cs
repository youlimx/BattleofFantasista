using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBody : MonoBehaviour
{
    int maxHP = 100;
    int currentHP;
    public Slider slider;

    void Start()
    {
        slider.value = 1;
        currentHP = maxHP;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            int damage = 1;

            currentHP = currentHP - damage;

            slider.value = (float)currentHP / (float)maxHP;

            if (currentHP == 0)
            {
                Debug.Log("GameOver");
                SceneManager.LoadScene("GameOver");
            }

        }
    }
}
