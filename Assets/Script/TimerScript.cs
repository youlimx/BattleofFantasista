using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    private float _countTime = 30;

    void Update()
    {
        _countTime -= Time.deltaTime; //�X�^�[�g���Ă���̕b�����i�[
        GetComponent<Text>().text = _countTime.ToString("F2"); //����2���ɂ��ĕ\��

        if (_countTime < 1)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }




}
