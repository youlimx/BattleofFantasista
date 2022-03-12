using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // public GameObject explosion;
    [SerializeField] private GameManager _gameManager;  //�Q�[���}�l�[�W���[
    [SerializeField] private int _enemyHP;              //�G�l�~�[��HP
    [SerializeField] private Slider _hpSlider;          //�G�l�~�[��HP�X���C�_�[        
    [SerializeField] private Animator _anim;            //�A�j���[�^
    [SerializeField] private AudioClip _breakSound;     //�G���|���ꂽ���̉�
    [SerializeField] private GameObject _beamSpark;     //�r�[�����������Ă���Ƃ��̃G�t�F�N�g

    private AudioSource _breakAudioSource;              //�G���|���ꂽ���̉��̉���
    private int _beamCount = 19;                        //�r�[�����o���Ă��鎞��

    void Start()
    {
        _hpSlider.maxValue = _enemyHP;
        _anim = GetComponent<Animator>();
        _breakAudioSource = GetComponent<AudioSource>();
        _gameManager = _gameManager.GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "RightArm")
        {
            Debug.Log(col.gameObject.name);
            _anim.Play("Damage", -1, 0);
            _enemyHP -= 1;
            Debug.Log("hit : " + _enemyHP);
            _hpSlider.value = _enemyHP;
            if (_enemyHP <= 0)
            {
                _breakAudioSource.PlayOneShot(_breakSound);
                //Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(transform.root.gameObject, 1.0f);
            }
        }
    }

    private void OnCollisionStay(Collision col)
    {

        if (col.gameObject.tag == "Beam")
        {

            _gameManager.AddScore(1);
            _beamCount++;
            if (_beamCount >= 20)
            {
                Debug.Log(col.gameObject.name);
                _anim.Play("Damage", -1, 0);
                _enemyHP -= 1;
                Debug.Log("EnemyHP: " + _enemyHP);
                _hpSlider.value = _enemyHP;
                _gameManager.BeamVibrate();

                Vector3 hitPos;

                foreach (ContactPoint point in col.contacts)
                {
                    hitPos = point.point;
                    hitPos += new Vector3(0, 0, -1.0f);
                    Instantiate(_beamSpark, hitPos, Quaternion.Euler(0, 180, 0));
                }

                _beamCount = 0;
            }

            }
        }
    }
