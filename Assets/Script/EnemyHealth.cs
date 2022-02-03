using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // public GameObject explosion;

    [SerializeField] private int _enemyHP;              //�G�l�~�[��HP
    [SerializeField] private Slider _hpSlider;          //�G�l�~�[��HP�X���C�_�[        
    [SerializeField] private Animator _anim;            //�A�j���[�^
    [SerializeField] private AudioClip _breakSound;     //�G���|���ꂽ���̉�

    private AudioSource _breakAudioSource;              //�G���|���ꂽ���̉��̉���
    private int trans;

    void Start()
    {
        //��Ŏg���܂�
        // hpSlider.maxValue = enemyHP;
        // hpSlider.value = enemyHP;
        _anim = GetComponent<Animator>();
        _breakAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(col.gameObject.name);
            _anim.Play("Damage", -1, 0);
            _enemyHP -= 1;
            Debug.Log("hit : " + _enemyHP);

            trans = 1;
            // hpSlider.value = enemyHP;
            if (_enemyHP <= 0)
            {
                _breakAudioSource.PlayOneShot(_breakSound);
                //Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(transform.root.gameObject, 1.0f);
            }
        }
    }
}
