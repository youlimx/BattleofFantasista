using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightArmScript : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;  //�Q�[���}�l�[�W���[
    [SerializeField] private AudioClip _punchSound;     //�p���`��
    [SerializeField] private GameObject _spark;         //�p���`�����Ƃ��ɏo��ΉԂ̃G�t�F�N�g

    private AudioSource _punchAudioSource;              //�p���`�̉��̉���

    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _punchAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _punchAudioSource.PlayOneShot(_punchSound);
            StartCoroutine(AttackVibrate(duration: 0.1f, controller: OVRInput.Controller.RTouch));
            _gameManager.AddScore(10);
            Instantiate(_spark, this.transform.position, Quaternion.identity);

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

