using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArmScript : MonoBehaviour
{
    GameObject GameManager;
    GameManager script;

    public AudioClip punchSound;
    AudioSource audioSource;

    public GameObject spark;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        script = GameManager.GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(punchSound);
            StartCoroutine(AttackVibrate(duration: 0.1f, controller: OVRInput.Controller.RTouch));
            script.Score();
            Instantiate(spark, this.transform.position, Quaternion.identity);

        }
    }

    public static IEnumerator AttackVibrate(float duration = 0.1f, float frequency = 1.0f, float amplitude = 1.0f, OVRInput.Controller controller = OVRInput.Controller.Active)
    {
        //�R���g���[���[��U��������
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        //�w�肳�ꂽ���ԑ҂�
        yield return new WaitForSeconds(duration);

        //�R���g���[���[�̐U�����~�߂�
        OVRInput.SetControllerVibration(0, 0, controller);

    }

}

