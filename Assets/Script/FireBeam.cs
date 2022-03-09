using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeam : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;  //�Q�[���}�l�[�W���[
    [SerializeField] private GameObject _leftArm;       //���r
    [SerializeField] private GameObject _beamPrefab;    //�r�[����prefab
    [SerializeField] private float _beamSpeed;          //�r�[���̑��x
    [SerializeField] private GameObject _spark;         //�r�[�����������Ă��鎞�̃G�t�F�N�g

    private bool _beamFire = false;   //�X�y�[�X�L�[�����������̔���
    private bool _beamTimer = false;  //�r�[���p�^�C�}�[�������Ă��邩�ǂ���
    private float _beamTime=3.75f;     //�r�[���p�^�C�}�[(���ۂ̒���+����)
    private GameObject _beam;         //�r�[����GameObject
    private ShakerScript _shaker;     //�h�炷�X�N���v�g

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
