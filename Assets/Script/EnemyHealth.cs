using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHP;
    public Slider hpSlider;
    public Animator anim;
    int trans=0;

    public AudioClip breakSound;
    AudioSource audioSource;

   // public GameObject explosion;

    void Start()
    {
        // hpSlider.maxValue = enemyHP;
        // hpSlider.value = enemyHP;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            anim.Play("Damage", -1, 0);
            enemyHP -= 1;
            Debug.Log("hit : " + enemyHP);

            trans = 1;
            // hpSlider.value = enemyHP;
            if (enemyHP <= 0)
            {
                audioSource.PlayOneShot(breakSound);
                //Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(transform.root.gameObject, 1.0f);
            }

        }
    }

}
