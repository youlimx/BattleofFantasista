using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeam : MonoBehaviour
{
    [SerializeField] GameObject beamPrefab;

    [SerializeField] float beamSpeed;

    bool onSpace = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !onSpace)
        {
            Debug.Log("onSpace");
            onSpace = true;

            GameObject beam = Instantiate(beamPrefab, transform.position, Quaternion.identity);

            Rigidbody beamRb = beam.GetComponent<Rigidbody>();

            beamRb.AddForce(transform.forward * beamSpeed);
            Destroy(beam, 2.0f);
        }
    }
}
