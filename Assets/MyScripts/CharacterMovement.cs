using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] float speed = 2f;

    [SerializeField] float gravity = -19f;

    [SerializeField] float jumpHeight = 1f;

    [SerializeField] Transform groundCheck;

    [SerializeField] float groundDistance = 4f;

    [SerializeField] LayerMask groundMask;

    [SerializeField] float RunningTime = 15; //koþma süresi



    [SerializeField] bool ChaLeansLeftOrRight = false;



    [SerializeField] AudioClip rapidSound;

  




    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 DirectionVector = new Vector3(x,0,z);

        controller.Move(DirectionVector * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(DirectionVector * speed * 3 * Time.deltaTime);

        }



    }

}
