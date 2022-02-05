using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeam : MonoBehaviour
{
    [SerializeField] private GameObject _beamPrefab;  //ビームのprefab
    [SerializeField] private float _beamSpeed;       //ビームの速度

    private bool _pushSpace = false;  //スペースキーを押したかの判定
    private float _beamTime=2.0f; //ビーム用タイマー


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && ! _pushSpace)
        {
            _pushSpace = true;
            GameObject beam = Instantiate(_beamPrefab, transform.position, Quaternion.identity);
            Rigidbody beamRb = beam.GetComponent<Rigidbody>();
            beamRb.AddForce(transform.forward * _beamSpeed);
            Destroy(beam,2.0f);
        }

        if (_pushSpace == true)
        {
            _beamTime -= Time.deltaTime;
        }

        if (_beamTime < 0)
        {
            _pushSpace = false;
            _beamTime = 2.0f;
        }
    }
}
