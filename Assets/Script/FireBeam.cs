using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeam : MonoBehaviour
{
    [SerializeField] GameObject _beamPrefab;

    [SerializeField] float _beamSpeed;

    private bool _onSpace = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && ! _onSpace)
        {
            Debug.Log("onSpace");
            _onSpace = true;

            GameObject beam = Instantiate(_beamPrefab, transform.position, Quaternion.identity);

            Rigidbody beamRb = beam.GetComponent<Rigidbody>();

            beamRb.AddForce(transform.forward * _beamSpeed);
            Destroy(beam, 2.0f);
        }
    }
}
