using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class PlayerScript : MonoBehaviour
{
    public Slider slider;       //�v���C���[��HP�X���C�_�[

    [SerializeField] private GameObject _beam;          //�r�[��
    [SerializeField] private float _speed = 10.0f;      //�ړ��̑���
    [SerializeField] private AudioClip _damageSound;    //�_���[�W���󂯂����̉�
    [SerializeField] private GameManager _gameManager;  //�Q�[���}�l�[�W���[

    private const int PunchDamage = 1;                  //�p���`�G�ɗ^����_���[�W
    

    private AudioSource _damageAudioSource;             //�v���C���[���_���[�W���󂯂����̉���
    private int _maxHP = 10;                            //�ő�HP
    private int _currentHP;                             //���݂�HP
    //private UDPServer udp;

    void Start()
    {
        _damageAudioSource = GetComponent<AudioSource>();

        //udp = GameObject.Find("Player").GetComponent<UDPServer>();

        slider.value = 1;
        _currentHP = _maxHP;

    }

    //�{�^�������������ɌĂяo���֐�
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
            Debug.Log("�Ȃ������I");
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

    //�v���C���[���ړ�������֐�
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

            //�V�[���J��
            if (_currentHP == 0)
            {
                // SceneManager.LoadScene("GameOver");
            }
        }
    }

    //�v���C���[���_���[�W���󂯂����̊֐�
    void RecieveDamage()
    {
        _currentHP = _currentHP - PunchDamage;

        slider.value = (float)_currentHP / (float)_maxHP;
    }


    

}
