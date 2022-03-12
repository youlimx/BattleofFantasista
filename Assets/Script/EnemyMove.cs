using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject _target;    //ターゲット(プレイヤー)
    [SerializeField] private GameObject _enemy;     //敵のオブジェクト

    private state _state = state.OUT_OF_RANGE;  //動きの状態。初期は範囲外
    private int _atkState = 0;                  //攻撃の状態
    private const int _atkLine = 15;            //攻撃範囲
    public Animator anim;                       //アニメーション

    float spd = 0.5f;       //速さ

    private float _standbyTimer = 0;  //待機用タイマー
    private float _stepTimer = 0;   //ステップ用タイマー
    private float _atkTimer = 0;    //攻撃用タイマー

    private enum state{
                    OUT_OF_RANGE,        //範囲外:0
                    ATK_LINE,           //攻撃を始めるライン:1
                    STEP_FORK,          //ステップの左右分岐:2
                    RIGHT_STEP,         //右ステップ:3
                    LEFT_STEP,          //左ステップ:4
                    RIGHT_TO_CENTER,    //右ステップからの中央:5
                    LEFT_TO_CENTER,     //左ステップからの中央:6
                    ATK                 //攻撃:7
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //敵の移動範囲の制限
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, -12, 12),
            Mathf.Clamp(this.transform.position.y, 0, 10), Mathf.Clamp(this.transform.position.z, -12, 12));
        
        EnemyMov();
    }

    void EnemyMov()
    {
        //常にプレイヤーのほうを向く
        transform.LookAt(_target.transform.position);
        
        if (_state == state.OUT_OF_RANGE) //攻撃範囲外での行動
        {
            //距離の取得
            Vector3 posE = this.transform.position;
            Vector3 posP = _target.transform.position;
            float dis = Vector3.Distance(posE, posP);

            //攻撃範囲外にいる場合
            if (dis > _atkLine)
            {
                //プレイヤーに向かって前進
                transform.position += transform.forward * spd;
            }
            else
            {
                //攻撃範囲のラインに着いたら
                _state = state.ATK_LINE;  //次の行動へ
            }
        }
        else if (_state == state.ATK_LINE)    //攻撃範囲のラインでの待機
        {
            _standbyTimer += Time.deltaTime;//待機の時間の計測
            _stepTimer = 0;                 //ステップ用タイマーをリセット
                                            //数秒待ったらPlayerに近づく
            if (_standbyTimer >= 2.0f)
            {
                //ランダムにステップか攻撃に分岐
                StateFork(state.STEP_FORK, state.ATK);
            }
        }
        //ステップの場合ランダムに右ステップか左ステップに分岐
        else if (_state == state.STEP_FORK)
        {
            StateFork(state.RIGHT_STEP, state.LEFT_STEP);
        }
        else if (_state == state.RIGHT_STEP)    //右ステップ
        {
            
            Step(state.RIGHT_STEP);
        }
        else if (_state == state.LEFT_STEP)     //左ステップ
        {
            Step(state.LEFT_STEP);
        }
        else if (_state == state.RIGHT_TO_CENTER)   //右から中央に戻る
        {
            StepToCenter(state.RIGHT_TO_CENTER);
        }
        else if (_state == state.LEFT_TO_CENTER)    //左から中央に戻る
        {
            StepToCenter(state.LEFT_TO_CENTER);
        }
        else if (_state == state.ATK)               //攻撃の場合
        {
            EnemyAttack();
        }
    }

    //分岐させる
    void StateFork(state stateA, state stateB)
    {
        //2つの状態のどちらに分岐するかランダムに決める
        int rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            //0の場合は状態A
            _state = stateA;
        }
        else if (rnd == 1)
        {
            //1の場合は状態B
            _state = stateB;
        }
    }

    //ステップする
    void Step(state step)
    {
        _stepTimer += Time.deltaTime;
        if (step == state.RIGHT_STEP)
        {
            anim.Play("RightStep");
            //右ステップ
            transform.position += transform.right * spd;

            if (_stepTimer > 1)
            {
                //右から中央に戻る
                _state = state.RIGHT_TO_CENTER;
            }
        }
        else if(step == state.LEFT_STEP)
        {
            anim.Play("LeftStep");
            //左ステップ
            transform.position -= transform.right * spd;

            if (_stepTimer > 1)
            {
                //左から中央に戻る
                _state = state.LEFT_TO_CENTER;
            }
        }
    }

    //ステップしてから中央に戻る
    void StepToCenter(state sayuu)
    {
        //進んだ時間と同じだけ反対に移動する
        _stepTimer -= Time.deltaTime;

        //左右での違い
        if (sayuu==state.RIGHT_TO_CENTER)
        {
            //左ステップのアニメーション
            anim.Play("LeftStep");
            //左移動
            transform.position -= transform.right * spd;
        }else if (sayuu == state.LEFT_TO_CENTER)
        {
            //右ステップのアニメーション
            anim.Play("RightStep");
            //左移動
            transform.position += transform.right * spd;
        }

        //中央に戻ったら
        if (_stepTimer < 0)
        {
            //ステップか攻撃か決めるところに戻る。
            _state = state.ATK_LINE;
        }
    }

    //攻撃
    void EnemyAttack()
    {
        _atkTimer += Time.deltaTime;
        if (_atkState == 0)
        {
            //距離の取得
            Vector3 posE = this.transform.position;
            Vector3 posP = _target.transform.position;
            float dis = Vector3.Distance(posE, posP);

            //前進する
            anim.Play("MoveForward");
            if (_atkTimer > 0.5f)
            {
                transform.position += transform.forward * spd;
            }
            //プレイヤーとの距離が2.5以下だったら
            if (dis < 2.5f)
            {
                _atkState = 1;  //攻撃する
            }
        }
        else if (_atkState == 1)
        {
            //攻撃
            anim.Play("Attack");
            if (_atkTimer >= 2.5f)
            {
                //攻撃に移行
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
