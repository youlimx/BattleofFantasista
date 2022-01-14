using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _enemyHP;
    [SerializeField] Slider _hpSlider;
    [SerializeField] Animator _anim;
    
    int trans=0;

    [SerializeField] AudioClip _breakSound;
    AudioSource _audioSource;

   // public GameObject explosion;

    void Start()
    {
        // hpSlider.maxValue = enemyHP;
        // hpSlider.value = enemyHP;
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    //private void OnCollisionStay(Collision col)
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
                _audioSource.PlayOneShot(_breakSound);
                //Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(transform.root.gameObject, 1.0f);
            }

        }
    }

}
