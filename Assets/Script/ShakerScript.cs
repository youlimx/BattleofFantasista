using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShakerScript : MonoBehaviour
{
    [SerializeField] private NoiseTransform _noisePosition;     //位置の揺れ情報
    [SerializeField] private NoiseTransform _noiseRotation;      //回転の揺れ情報 

    //Transformの初期状態
    private Transform _transform;
    private Vector3 _initLocalPosition;
    private Quaternion _initLocalQuaternion;

    //振動のしてるかどうかのフラグ
    public bool flag = false;

    [Serializable]
    private struct NoiseParam
    {
        public float amplitude;  //振幅
        public float speed;      //振幅の速さ 
        private float _offset;   //パーリンノイズのオフセット

        //乱数のオフセット値を指定する
        public void SetRandomOffset()
        {
            _offset = UnityEngine.Random.Range(0f, 256f);
        }

        //指定時刻のパーリンノイズ値を所得する
        public float GetValue(float time)
        {
            //ノイズ位置を計算
            var noisePos = speed * time + _offset;

            //-1~1の範囲のノイズ値を取得
            var noiseValue = 2 * (Mathf.PerlinNoise(noisePos, 0) - 0.5f);

            //振幅を掛けた値を返却
            return amplitude * noiseValue;
        }
    }

    //パーリンノイズのXYZ情報
    [Serializable]
    private struct NoiseTransform
    {
        public NoiseParam x, y, z;

        public void SetRandomOffset()
        {
            x.SetRandomOffset();
            y.SetRandomOffset();
            z.SetRandomOffset();
        }

        //指定時刻のパーリンノイズ値を取得する
        public Vector3 GetValue(float time)
        {
            return new Vector3(
              x.GetValue(time),
              y.GetValue(time),
              z.GetValue(time)
                );
        }
    }

    //初期化
    private void Awake()
    {
        _transform = transform;

        //Transformの初期値を保持
        _initLocalPosition = _transform.localPosition;
        _initLocalQuaternion = _transform.localRotation;

        //パーリンノイズのオフセット初期化
        _noisePosition.SetRandomOffset();
        _noiseRotation.SetRandomOffset();
    }

    private void Update()
    {
        if (flag==true)
        {
            //ゲーム開始からの時間取得
            var time = Time.time;

            //パーリンノイズの値を時刻から取得
            var noisePos = _noisePosition.GetValue(time);
            var noiseRot = _noisePosition.GetValue(time);

            //各Transformにパーリンノイズの値を加算
            _transform.localPosition = _initLocalPosition + noisePos;
            _transform.localRotation = Quaternion.Euler(noiseRot) * _initLocalQuaternion;
        }
    }
}
