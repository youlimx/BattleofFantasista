using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightArmScript : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;  //ゲームマネージャー
    [SerializeField] private AudioClip _punchSound;     //パンチ音
    [SerializeField] private GameObject _spark;         //パンチしたときに出る火花のエフェクト
    [SerializeField] private Animator _gripAnim;        //手を握るアニメーション

    private AudioSource _punchAudioSource;              //パンチの音の音源
    private float _rTrigger;                            //右コントローラーのトリガーボタンの入力

    void Start()
    {
        _punchAudioSource = GetComponent<AudioSource>();
        _gripAnim = GetComponent<Animator>();
    }

    void Update()
    {
         _rTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger); 
        Debug.Log("Trigger:" + _rTrigger);
        if (Input.GetKey(KeyCode.Return) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            _gripAnim.SetBool("blGrp", true);
        }
        else
        {
            _gripAnim.SetBool("blGrp", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Input.GetKey(KeyCode.Return) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            _gripAnim.SetBool("blGrp", true);
            if (collision.gameObject.tag == "Enemy")
            {
                _punchAudioSource.PlayOneShot(_punchSound);
                _gameManager.AddScore(10);
                Instantiate(_spark, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z+1), Quaternion.identity);
            }
        }
    }

}

