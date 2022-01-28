using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text _scoreText;    //スコア用のテキスト
 
    private int score;                  //スコアをカウントする用の変数

    void Update()
    {
        PlayerPrefs.SetInt("score", score);
        _scoreText.text = score.ToString("f0");
    }

    //スコアを足す関数
    public void AddScore(int value)
    {
        score += value;
    }

}
