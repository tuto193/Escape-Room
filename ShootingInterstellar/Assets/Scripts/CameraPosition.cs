using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform ballTransform;
    private Transform _cameraTransform;
    private Vector3 _initialOffset;

    private void Start()
    {
        _cameraTransform = GetComponent<Transform>();
        _initialOffset = ballTransform.position - _cameraTransform.position;
    }

    private void Update()
    {
        _cameraTransform.position = ballTransform.position - _initialOffset;
    }
    
}
