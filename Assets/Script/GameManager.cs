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
    public void Vibrate()
    {
        StartCoroutine(AttackVibrate(0.1f, 1.0f, 1.0f, controller: OVRInput.Controller.RTouch));
    }

    public static IEnumerator AttackVibrate(float duration, float frequency, float amplitude, OVRInput.Controller controller = OVRInput.Controller.Active)
    {
        //�R���g���[���[��U��������
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        //�w�肳�ꂽ���ԑ҂�
        yield return new WaitForSeconds(duration);

        //�R���g���[���[�̐U�����~�߂�
        OVRInput.SetControllerVibration(0, 0, controller);

    }

}
