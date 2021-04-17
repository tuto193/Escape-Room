using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private bool can_move;
    public bool CanMove { get{return can_move;} set{can_move = value;} }

    public float speed = 12f;
    // To interact with Pictures
    public GameObject interactablePic;
    // Update is called once per frame

    private void Start()
    {
        can_move = true;
    }

    void Update()
    {
        if (!can_move) {
            return;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * (speed * Time.deltaTime));
        //_source.clip = steps;
        //_source.Play();


    }

}
