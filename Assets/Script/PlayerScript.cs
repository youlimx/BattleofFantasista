using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class PlayerScript : MonoBehaviour
{
    public Slider slider;       //プレイヤーのHPスライダー

    [SerializeField] private GameObject _beam;          //ビーム
    [SerializeField] private float _speed = 10.0f;      //移動の速さ
    [SerializeField] private AudioClip _damageSound;    //ダメージを受けた時の音
    [SerializeField] private GameManager _gameManager;  //ゲームマネージャー

    private const int PunchDamage = 1;                  //パンチ敵に与えるダメージ
    

    private AudioSource _damageAudioSource;             //プレイヤーがダメージを受けた時の音源
    private int _maxHP = 10;                            //最大HP
    private int _currentHP;                             //現在のHP
    //private UDPServer udp;

    void Start()
    {
        _damageAudioSource = GetComponent<AudioSource>();

        //udp = GameObject.Find("Player").GetComponent<UDPServer>();

        slider.value = 1;
        _currentHP = _maxHP;

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

    void Update()
    {

        if (Input.GetKey("up"))
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
        if (Input.GetKey("down"))
        {
            transform.position -= transform.forward * _speed * Time.deltaTime;
        }
        
        if (Input.GetKey("right"))
        {
            transform.position += transform.right * _speed * Time.deltaTime;
        }
        if (Input.GetKey("left"))
        {
            transform.position -= transform.right * _speed * Time.deltaTime;
        }
        
        transform.position += (Vector3)moveDirection * 0.5f * Time.deltaTime;
    }

    Vector2 moveDirection = Vector2.zero;

    //プレイヤーを移動させる関数
   void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
    }


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
