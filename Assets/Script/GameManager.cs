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
    public void Vibrate()
    {
        StartCoroutine(AttackVibrate(0.1f, 1.0f, 1.0f, controller: OVRInput.Controller.RTouch));
    }

    public static IEnumerator AttackVibrate(float duration, float frequency, float amplitude, OVRInput.Controller controller = OVRInput.Controller.Active)
    {
        //コントローラーを振動させる
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        //指定された時間待つ
        yield return new WaitForSeconds(duration);

        //コントローラーの振動を止める
        OVRInput.SetControllerVibration(0, 0, controller);

    }

}
