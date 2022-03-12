using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;      //移動の速さ

    //前進しているときの状態と加速度
    public int _moveFront = -1;
    public float _velosityFront = 0.0f;

    //後退しているときの状態と加速度
    public int _moveBack = -1;
    public float _velosityBack = 0.0f;

    //右移動のときの状態と加速度
    public int _moveRight = -1;
    public float _velosityRight = 0.0f;

    //左移動のときの状態と加速度
    public int _moveLeft = -1;
    public float _velosityLeft = 0.0f;

    public float _add_velosity = 0.5f;  //加速度を加算する量
    public float _max_velosity = 10.0f; //最大の加速度
    public float _decrease_velosity = 0.5f; //加速度を減算

    public void Move()
    {
        //移動範囲制限
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, -12, 12),
            Mathf.Clamp(this.transform.position.y, 0, 10), Mathf.Clamp(this.transform.position.z, -12, 12));

        //矢印キーor左スティックで移動
        Vector2 stickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        if (Input.GetKey("up") || stickL.y > 0.5f)
        {
            _moveFront = 0;
        }
        if (Input.GetKey("down") || stickL.y < -0.5f)
        {
            _moveBack = 0;
        }
        if (Input.GetKey("right") || stickL.x > 0.5f)
        {
            _moveRight = 0;
        }
        if (Input.GetKey("left") || stickL.x < -0.5f)
        {
            _moveLeft = 0;
        }

        //前進のとき
        if (_moveFront == 0)    //進む
        {
            _velosityFront += _add_velosity;
            if (_velosityFront >= _max_velosity)
            {
                _velosityFront = _max_velosity;
                _moveFront = 1;
            }
        }
        else if (_moveFront == 1)   //止まっても少しの間は進む
        {
            _velosityFront += -_decrease_velosity;
            if (_velosityFront <= 0.2f)
            {
                _moveFront = -1;
                _velosityFront = 0;
            }
        }

        //後退のとき
        if (_moveBack == 0)
        {
            _velosityBack += _add_velosity;
            if (_velosityBack >= _max_velosity)
            {
                _velosityBack = _max_velosity;
                _moveBack = 1;
            }
        }
        else if (_moveBack == 1)
        {
            _velosityBack += -_decrease_velosity;
            if (_velosityBack <= 0.2f)
            {
                _moveBack = -1;
                _velosityBack = 0;
            }
        }

        //右移動のとき
        if (_moveRight == 0)
        {
            _velosityRight += _add_velosity;
            if (_velosityRight >= _max_velosity)
            {
                _velosityRight = _max_velosity;
                _moveRight = 1;
            }

        }
        else if (_moveRight == 1)
        {
            _velosityRight += -_decrease_velosity;
            if (_velosityRight <= 0.2f)
            {
                _moveRight = -1;
                _velosityRight = 0;
            }
        }

        //左移動のとき
        if (_moveLeft == 0)
        {
            _velosityLeft += _add_velosity;
            if (_velosityLeft >= _max_velosity)
            {
                _velosityLeft = _max_velosity;
                _moveLeft = 1;
            }
        }
        else if (_moveLeft == 1)
        {
            _velosityLeft += -_decrease_velosity;
            if (_velosityLeft <= 0.2f)
            {
                _moveLeft = -1;
                _velosityLeft = 0;
            }
        }

        //それぞれに移動量を加算する
        transform.position += transform.forward * _velosityFront * Time.deltaTime;
        transform.position += transform.forward * -_velosityBack * Time.deltaTime;
        transform.position += transform.right * _velosityRight * Time.deltaTime;
        transform.position += transform.right * -_velosityLeft * Time.deltaTime;

        //右スティックで回転
        Vector2 stickR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        transform.Rotate(new Vector3(0, stickR.x * 2, 0));

        Vector3 changeRotation = new Vector3(0, InputTracking.GetLocalRotation(XRNode.Head).eulerAngles.y, 0);
    }
}
