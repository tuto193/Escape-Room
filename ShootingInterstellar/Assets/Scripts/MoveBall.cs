using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float forceMultiplier;
    public float jumpStrength;
   
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody could not be found");
        }
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        _rigidbody.AddForce(new Vector3(x * forceMultiplier, 0, y * forceMultiplier));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(new Vector3(0,1,0) * jumpStrength, ForceMode.Impulse);
        }
    }

    
}
