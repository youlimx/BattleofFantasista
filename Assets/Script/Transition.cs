/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{

    private void Update()
    {
        //GetComponent��p����Animator�R���|�[�l���g�����o��.
        Animator animator = GetComponent<Animator>();

        //���炩���ߐݒ肵�Ă���int�p�����[�^�[�utrans�v�̒l�����o��.
        int trans = animator.GetInteger("trans");

        //����L�[���������ۂɃp�����[�^�utrans�v�̒l�𑝉�������.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            trans++;
        }

        //�����L�[���������ۂɃp�����[�^�utrans�v�̒l������������.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            trans--;
        }

        //int�p�����[�^�[�̒l��ݒ肷��.
        animator.SetInteger("trans", trans);
    }
}
*/