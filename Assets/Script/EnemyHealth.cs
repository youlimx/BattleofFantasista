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
    private bool _defeat = false;                       //敵が倒されたかどうか。

    void Start()
    {
        _hpSlider.maxValue = _enemyHP;
        _anim = GetComponent<Animator>();
        _breakAudioSource = GetComponent<AudioSource>();
        _gameManager = _gameManager.GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (Input.GetKey(KeyCode.Return) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            if (col.gameObject.tag == "RightArm")
            {
                _anim.Play("Damage", -1, 0);
                _enemyHP -= 1;
                _hpSlider.value = _enemyHP;
                if (_enemyHP <= 0 && _defeat == false)
                {
                    _defeat = true;
                    EnemyDefeat();
                }
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
                _anim.Play("Damage", -1, 0);
                _enemyHP -= 1;
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
                if (_enemyHP <= 0 && _defeat == false)
                {
                    _defeat = true;
                    EnemyDefeat();
                }

            }
        }
    }

    void EnemyDefeat()
    {
        _gameManager.result.text = "YOU WIN!";
        Destroy(this.gameObject, 1.0f);

        /*2体目の敵を出すときに使う。
        GameObject.Find("STAGE").SetActive(false);
        RenderSettings.skybox = sky;
        Instantiate(enemy2, new Vector3(0, 0, 0), Quaternion.identity);
        */
    }
}