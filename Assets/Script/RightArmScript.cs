using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightArmScript : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;  //ゲームマネージャー
    [SerializeField] private AudioClip _punchSound;     //パンチ音
    [SerializeField] private GameObject _spark;         //パンチしたときに出る火花のエフェクト

    private AudioSource _punchAudioSource;              //パンチの音の音源

    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _punchAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _punchAudioSource.PlayOneShot(_punchSound);
            StartCoroutine(AttackVibrate(duration: 0.1f, controller: OVRInput.Controller.RTouch));
            _gameManager.AddScore(10);
            Instantiate(_spark, this.transform.position, Quaternion.identity);

        }
    }

    public static IEnumerator AttackVibrate(float duration = 0.1f, float frequency = 1.0f, float amplitude = 1.0f, OVRInput.Controller controller = OVRInput.Controller.Active)
    {
        //コントローラーを振動させる
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        //指定された時間待つ
        yield return new WaitForSeconds(duration);

        //コントローラーの振動を止める
        OVRInput.SetControllerVibration(0, 0, controller);

    }

}

