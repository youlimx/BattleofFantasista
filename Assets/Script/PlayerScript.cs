using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerScript : MonoBehaviour
{
    public Slider slider;       //プレイヤーのHPスライダー
    public PlayerMove move;

    [SerializeField] private GameObject _beam;          //ビーム
    [SerializeField] private AudioClip _damageSound;    //ダメージを受けた時の音
    [SerializeField] private GameManager _gameManager;  //ゲームマネージャー
    [SerializeField] private GameObject _player;

    private const int PunchDamage = 1;                  //パンチ敵に与えるダメージ

    private AudioSource _damageAudioSource;             //プレイヤーがダメージを受けた時の音源
    private int _maxHP = 10;                            //最大HP
    private int _currentHP;                             //現在のHP
    //private UDPServer udp;

    void Start()
    {
        _damageAudioSource = GetComponent<AudioSource>();
        move = _player.GetComponent<PlayerMove>();

        //udp = GameObject.Find("Player").GetComponent<UDPServer>();

        slider.value = 1;
        _currentHP = _maxHP;
    }

    private void Update()
    {
        move.Move();
    }

    //ボタンを押した時に呼び出す関数
    public void Button()
    {
        Instantiate(_beam, (this.transform.position + transform.up * 0.5f), this.transform.rotation);
    }

    /*
    void Udp()
    {
        Debug.Log(udp.mageval);
        if (udp.mageval > 700)
        {
            Debug.Log("曲がった！");
            // transform.position += transform.forward * udp.mageval * Time.deltaTime;
            Instantiate(beam, (this.transform.position + transform.forward * 0.5f), this.transform.rotation);
            Destroy(this.gameObject, 10.0f);
        }
    }*/


    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            _gameManager.PunchVibrate();
            _damageAudioSource.PlayOneShot(_damageSound);

            RecieveDamage();

            //シーン遷移
            if (_currentHP == 0)
            {
                // SceneManager.LoadScene("GameOver");
            }
        }
    }

    //プレイヤーがダメージを受けた時の関数
    void RecieveDamage()
    {
        _currentHP = _currentHP - PunchDamage;

        slider.value = (float)_currentHP / (float)_maxHP;
    }

}
