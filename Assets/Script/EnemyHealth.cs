using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // public GameObject explosion;

    [SerializeField] private int _enemyHP;              //エネミーのHP
    [SerializeField] private Slider _hpSlider;          //エネミーのHPスライダー        
    [SerializeField] private Animator _anim;            //アニメータ
    [SerializeField] private AudioClip _breakSound;     //敵が倒された時の音

    private AudioSource _breakAudioSource;              //敵が倒された時の音の音源

    void Start()
    {
        _hpSlider.maxValue = _enemyHP;
        _anim = GetComponent<Animator>();
        _breakAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "RightArm")
        {
            Debug.Log(col.gameObject.name);
            _anim.Play("Damage", -1, 0);
            _enemyHP -= 1;
            Debug.Log("hit : " + _enemyHP);
            _hpSlider.value = _enemyHP;
            if (_enemyHP <= 0)
            {
                _breakAudioSource.PlayOneShot(_breakSound);
                //Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(transform.root.gameObject, 1.0f);
            }
        }
    }
}
