using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text _scoreText;    //�X�R�A�p�̃e�L�X�g
 
    private int score;                  //�X�R�A���J�E���g����p�̕ϐ�

    void Update()
    {
        PlayerPrefs.SetInt("score", score);
        _scoreText.text = score.ToString("f0");
    }

    //�X�R�A�𑫂��֐�
    public void AddScore(int value)
    {
        score += value;
    }

}
