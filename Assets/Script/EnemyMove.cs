using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject _target;    //�^�[�Q�b�g(�v���C���[)
    [SerializeField] private GameObject _enemy;     //�G�̃I�u�W�F�N�g

    private state _state = state.OUT_OF_RANGE;  //�����̏�ԁB�����͔͈͊O
    private int _atkState = 0;                  //�U���̏��
    private const int _atkLine = 15;            //�U���͈�
    public Animator anim;                       //�A�j���[�V����

    float spd = 0.5f;       //����

    private float _standbyTimer = 0;  //�ҋ@�p�^�C�}�[
    private float _stepTimer = 0;   //�X�e�b�v�p�^�C�}�[
    private float _atkTimer = 0;    //�U���p�^�C�}�[

    private enum state{
                    OUT_OF_RANGE,        //�͈͊O:0
                    ATK_LINE,           //�U�����n�߂郉�C��:1
                    STEP_FORK,          //�X�e�b�v�̍��E����:2
                    RIGHT_STEP,         //�E�X�e�b�v:3
                    LEFT_STEP,          //���X�e�b�v:4
                    RIGHT_TO_CENTER,    //�E�X�e�b�v����̒���:5
                    LEFT_TO_CENTER,     //���X�e�b�v����̒���:6
                    ATK                 //�U��:7
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //�G�̈ړ��͈͂̐���
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, -12, 12),
            Mathf.Clamp(this.transform.position.y, 0, 10), Mathf.Clamp(this.transform.position.z, -12, 12));
        
        EnemyMov();
    }

    void EnemyMov()
    {
        //��Ƀv���C���[�̂ق�������
        transform.LookAt(_target.transform.position);
        
        if (_state == state.OUT_OF_RANGE) //�U���͈͊O�ł̍s��
        {
            //�����̎擾
            Vector3 posE = this.transform.position;
            Vector3 posP = _target.transform.position;
            float dis = Vector3.Distance(posE, posP);

            //�U���͈͊O�ɂ���ꍇ
            if (dis > _atkLine)
            {
                //�v���C���[�Ɍ������đO�i
                transform.position += transform.forward * spd;
            }
            else
            {
                //�U���͈͂̃��C���ɒ�������
                _state = state.ATK_LINE;  //���̍s����
            }
        }
        else if (_state == state.ATK_LINE)    //�U���͈͂̃��C���ł̑ҋ@
        {
            _standbyTimer += Time.deltaTime;//�ҋ@�̎��Ԃ̌v��
            _stepTimer = 0;                 //�X�e�b�v�p�^�C�}�[�����Z�b�g
                                            //���b�҂�����Player�ɋ߂Â�
            if (_standbyTimer >= 2.0f)
            {
                //�����_���ɃX�e�b�v���U���ɕ���
                StateFork(state.STEP_FORK, state.ATK);
            }
        }
        //�X�e�b�v�̏ꍇ�����_���ɉE�X�e�b�v�����X�e�b�v�ɕ���
        else if (_state == state.STEP_FORK)
        {
            StateFork(state.RIGHT_STEP, state.LEFT_STEP);
        }
        else if (_state == state.RIGHT_STEP)    //�E�X�e�b�v
        {
            
            Step(state.RIGHT_STEP);
        }
        else if (_state == state.LEFT_STEP)     //���X�e�b�v
        {
            Step(state.LEFT_STEP);
        }
        else if (_state == state.RIGHT_TO_CENTER)   //�E���璆���ɖ߂�
        {
            StepToCenter(state.RIGHT_TO_CENTER);
        }
        else if (_state == state.LEFT_TO_CENTER)    //�����璆���ɖ߂�
        {
            StepToCenter(state.LEFT_TO_CENTER);
        }
        else if (_state == state.ATK)               //�U���̏ꍇ
        {
            EnemyAttack();
        }
    }

    //���򂳂���
    void StateFork(state stateA, state stateB)
    {
        //2�̏�Ԃ̂ǂ���ɕ��򂷂邩�����_���Ɍ��߂�
        int rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            //0�̏ꍇ�͏��A
            _state = stateA;
        }
        else if (rnd == 1)
        {
            //1�̏ꍇ�͏��B
            _state = stateB;
        }
    }

    //�X�e�b�v����
    void Step(state step)
    {
        _stepTimer += Time.deltaTime;
        if (step == state.RIGHT_STEP)
        {
            anim.Play("RightStep");
            //�E�X�e�b�v
            transform.position += transform.right * spd;

            if (_stepTimer > 1)
            {
                //�E���璆���ɖ߂�
                _state = state.RIGHT_TO_CENTER;
            }
        }
        else if(step == state.LEFT_STEP)
        {
            anim.Play("LeftStep");
            //���X�e�b�v
            transform.position -= transform.right * spd;

            if (_stepTimer > 1)
            {
                //�����璆���ɖ߂�
                _state = state.LEFT_TO_CENTER;
            }
        }
    }

    //�X�e�b�v���Ă��璆���ɖ߂�
    void StepToCenter(state sayuu)
    {
        //�i�񂾎��ԂƓ����������΂Ɉړ�����
        _stepTimer -= Time.deltaTime;

        //���E�ł̈Ⴂ
        if (sayuu==state.RIGHT_TO_CENTER)
        {
            //���X�e�b�v�̃A�j���[�V����
            anim.Play("LeftStep");
            //���ړ�
            transform.position -= transform.right * spd;
        }else if (sayuu == state.LEFT_TO_CENTER)
        {
            //�E�X�e�b�v�̃A�j���[�V����
            anim.Play("RightStep");
            //���ړ�
            transform.position += transform.right * spd;
        }

        //�����ɖ߂�����
        if (_stepTimer < 0)
        {
            //�X�e�b�v���U�������߂�Ƃ���ɖ߂�B
            _state = state.ATK_LINE;
        }
    }

    //�U��
    void EnemyAttack()
    {
        _atkTimer += Time.deltaTime;
        if (_atkState == 0)
        {
            //�����̎擾
            Vector3 posE = this.transform.position;
            Vector3 posP = _target.transform.position;
            float dis = Vector3.Distance(posE, posP);

            //�O�i����
            anim.Play("MoveForward");
            if (_atkTimer > 0.5f)
            {
                transform.position += transform.forward * spd;
            }
            //�v���C���[�Ƃ̋�����2.5�ȉ���������
            if (dis < 2.5f)
            {
                _atkState = 1;  //�U������
            }
        }
        else if (_atkState == 1)
        {
            //�U��
            anim.Play("Attack");
            if (_atkTimer >= 2.5f)
            {
                //�U���Ɉڍs
                _standbyTimer = 0;
                _atkState = 2;
            }
        }
        else if (_atkState == 2)
        {
            anim.Play("Back");
            transform.position += transform.forward * -0.35f;
            Vector3 posE = this.transform.position;
            Vector3 posP = _target.transform.position;
            float dis = Vector3.Distance(posE, posP);
            if (dis > _atkLine)
            {
                anim.Play("Standby");
                _atkState = 0;
                _atkTimer = 0;
                _state = state.OUT_OF_RANGE;
            }
        }
    }
}
