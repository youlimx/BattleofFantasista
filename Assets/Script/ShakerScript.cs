using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShakerScript : MonoBehaviour
{
    [SerializeField] private NoiseTransform _noisePosition;     //�ʒu�̗h����
    [SerializeField] private NoiseTransform _noiseRotation;      //��]�̗h���� 

    //Transform�̏������
    private Transform _transform;
    private Vector3 _initLocalPosition;
    private Quaternion _initLocalQuaternion;

    //�U���̂��Ă邩�ǂ����̃t���O
    public bool flag = false;

    [Serializable]
    private struct NoiseParam
    {
        public float amplitude;  //�U��
        public float speed;      //�U���̑��� 
        private float _offset;   //�p�[�����m�C�Y�̃I�t�Z�b�g

        //�����̃I�t�Z�b�g�l���w�肷��
        public void SetRandomOffset()
        {
            _offset = UnityEngine.Random.Range(0f, 256f);
        }

        //�w�莞���̃p�[�����m�C�Y�l����������
        public float GetValue(float time)
        {
            //�m�C�Y�ʒu���v�Z
            var noisePos = speed * time + _offset;

            //-1~1�͈̔͂̃m�C�Y�l���擾
            var noiseValue = 2 * (Mathf.PerlinNoise(noisePos, 0) - 0.5f);

            //�U�����|�����l��ԋp
            return amplitude * noiseValue;
        }
    }

    //�p�[�����m�C�Y��XYZ���
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

        //�w�莞���̃p�[�����m�C�Y�l���擾����
        public Vector3 GetValue(float time)
        {
            return new Vector3(
              x.GetValue(time),
              y.GetValue(time),
              z.GetValue(time)
                );
        }
    }

    //������
    private void Awake()
    {
        _transform = transform;

        //Transform�̏����l��ێ�
        _initLocalPosition = _transform.localPosition;
        _initLocalQuaternion = _transform.localRotation;

        //�p�[�����m�C�Y�̃I�t�Z�b�g������
        _noisePosition.SetRandomOffset();
        _noiseRotation.SetRandomOffset();
    }

    private void Update()
    {
        if (flag==true)
        {
            //�Q�[���J�n����̎��Ԏ擾
            var time = Time.time;

            //�p�[�����m�C�Y�̒l����������擾
            var noisePos = _noisePosition.GetValue(time);
            var noiseRot = _noisePosition.GetValue(time);

            //�eTransform�Ƀp�[�����m�C�Y�̒l�����Z
            _transform.localPosition = _initLocalPosition + noisePos;
            _transform.localRotation = Quaternion.Euler(noiseRot) * _initLocalQuaternion;
        }
    }
}
