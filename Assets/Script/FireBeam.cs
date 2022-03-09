using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeam : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;  //ゲームマネージャー
    [SerializeField] private GameObject _leftArm;       //左腕
    [SerializeField] private GameObject _beamPrefab;    //ビームのprefab
    [SerializeField] private float _beamSpeed;          //ビームの速度
    [SerializeField] private GameObject _spark;         //ビームが当たっている時のエフェクト

    private bool _beamFire = false;   //スペースキーを押したかの判定
    private bool _beamTimer = false;  //ビーム用タイマーが動いているかどうか
    private float _beamTime=3.75f;     //ビーム用タイマー(実際の長さ+反動)
    private GameObject _beam;         //ビームのGameObject
    private ShakerScript _shaker;     //揺らすスクリプト

    private void Start()
    {
        _gameManager = _gameManager.GetComponent<GameManager>();
        _shaker = _leftArm.GetComponent<ShakerScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _gameManager.AddScore(10);
        Instantiate(_spark, this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Beam") && ! _beamFire)
        {
            _beamFire = true;
            _beamTimer = true;
            _shaker.flag = true;
            _beam = Instantiate(_beamPrefab, transform.position, Quaternion.identity);
            _beam.transform.parent = transform;
            _beam.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _beam.transform.rotation = default;
            Destroy(_beam,3.5f);
        }

        if (_beamTimer == true)
        {
            _beamTime -= Time.deltaTime;
        }
        if (_beamTime < 0.25f && _beamFire == true)
        {
            _beamFire = false;
        }
        if (_beamTime < 0 && _beamFire == false)
        {
            _shaker.flag = false;
            _beamTimer = false;
            _beamTime = 3.75f;
        }
    }
}
