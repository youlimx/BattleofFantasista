using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightArmScript : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;  //ゲームマネージャー
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
            _gameManager.Vibrate();
            Debug.Log("Vibrate");
            _gameManager.AddScore(10);
            Instantiate(_spark, this.transform.position, Quaternion.identity);

        }
    }

}

