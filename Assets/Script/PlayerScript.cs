using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    private int _maxHP = 10;
    private int _currentHP;
    public Slider slider;

    [SerializeField] GameObject _beam;

    //UDPServer udp;
    [SerializeField] float _speed = 10.0f;

    [SerializeField] AudioClip _damageSound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        //udp = GameObject.Find("Ninja").GetComponent<UDPServer>();

        slider.value = 1;
        _currentHP = _maxHP;

    }

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
            Debug.Log("曲がった！");
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

   void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
    }

    //if (Input.GetKey("left"))
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            StartCoroutine(AttackVibrate(duration: 0.3f, controller: OVRInput.Controller.RTouch));
            _audioSource.PlayOneShot(_damageSound);
            int damage = 1;

            _currentHP = _currentHP - damage;

            slider.value = (float)_currentHP / (float)_maxHP;



            if (_currentHP == 0)
            {
               // SceneManager.LoadScene("GameOver");
            }

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
