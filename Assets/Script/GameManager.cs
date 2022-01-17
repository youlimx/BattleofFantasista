using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;

    private int count;

    void Update()
    {
        PlayerPrefs.SetInt("score", count);
        scoreText.text = count.ToString("f0");
    }

    public void Score()
    {
        count += 10;
    }
}
