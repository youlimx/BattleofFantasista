using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    float countTime = 30;

    void Update()
    {
        countTime -= Time.deltaTime; //�X�^�[�g���Ă���̕b�����i�[
        GetComponent<Text>().text = countTime.ToString("F2"); //����2���ɂ��ĕ\��

        if (countTime < 1)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }




}
