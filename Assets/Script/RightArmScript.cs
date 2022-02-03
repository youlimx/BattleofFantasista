using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightArmScript : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;  //�Q�[���}�l�[�W���[
    [SerializeField] private AudioClip _punchSound;     //�p���`��
    [SerializeField] private GameObject _spark;         //�p���`�����Ƃ��ɏo��ΉԂ̃G�t�F�N�g

    private AudioSource _punchAudioSource;              //�p���`�̉��̉���

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

