using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // public GameObject explosion;
    [SerializeField] private GameManager _gameManager;  //ゲームマネージャー
    [SerializeField] private int _enemyHP;              //エネミーのHP
    [SerializeField] private Slider _hpSlider;          //エネミーのHPスライダー        
    [SerializeField] private Animator _anim;            //アニメータ
    [SerializeField] private AudioClip _breakSound;     //敵が倒された時の音
    [SerializeField] private GameObject _beamSpark;     //ビームが当たっているときのエフェクト

    private AudioSource _breakAudioSource;              //敵が倒された時の音の音源
    private int _beamCount = 19;                        //ビームを出している時間

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
