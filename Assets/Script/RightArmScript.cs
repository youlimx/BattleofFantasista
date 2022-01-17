using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArmScript : MonoBehaviour
{
    [SerializeField] private AudioClip _punchSound;
    [SerializeField] private GameObject _spark;

    private GameObject _gameManager;
    private GameManager _script;
    private AudioSource _audioSource;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager");
        _script = _gameManager.GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _audioSource.PlayOneShot(_punchSound);
            StartCoroutine(AttackVibrate(duration: 0.1f, controller: OVRInput.Controller.RTouch));
            _script.Score();
            Instantiate(_spark, this.transform.position, Quaternion.identity);

        }
    }

    public static IEnumerator AttackVibrate(float duration = 0.1f, float frequency = 1.0f, float amplitude = 1.0f, OVRInput.Controller controller = OVRInput.Controller.Active)
    {
        //コントローラーを振動させる
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        //指定された時間待つ
        yield return new WaitForSeconds(duration);

        //コントローラーの振動を止める
        OVRInput.SetControllerVibration(0, 0, controller);

    }

}

