using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip steps;
    public CharacterController controller;

    public float speed = 12f;
    // To interact with Pictures 
    public GameObject interactablePic; 
    // Update is called once per frame

    private void Start()
    {
        _source = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * (speed * Time.deltaTime));
        //_source.clip = steps;
        //_source.Play();

        
    }
    
}