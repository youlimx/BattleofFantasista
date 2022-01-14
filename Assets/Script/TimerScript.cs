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
        _countTime -= Time.deltaTime; //スタートしてからの秒数を格納
        GetComponent<Text>().text = _countTime.ToString("F2"); //小数2桁にして表示

        if (_countTime < 1)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }




}
