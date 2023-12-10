using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] float speed = 8f;

    [SerializeField] float gravity = -9.81f;

    [SerializeField] float jumpHeight = 1f;

    [SerializeField] Transform groundCheck;

    [SerializeField] float groundDistance = 4f;

    [SerializeField] LayerMask groundMask;

    [SerializeField] float RunningTime = 15; //koþma süresi

    bool characterisrunning = false;


    [SerializeField] AudioClip rapidSound;
    private AudioSource sound;

    bool isGrounded;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        if (Input.GetKey(KeyCode.LeftShift))
        {
            RunningTime -= Time.deltaTime;
            speed += 15 * Time.deltaTime; //hýzýnýn bi anda 20 ye fýrlamasý yerine 1 2 sayine içerisinde yavaþ yavaþ 20 ye çýkmasý için 
            speed = Mathf.Clamp(speed, 8, 20);
            characterisrunning = true;

            if (RunningTime <= 0)
            {
                characterisrunning = false;
                RunningTime = 0;


                if (!sound.isPlaying)
                {
                    sound.PlayOneShot(rapidSound, 0.2f);
                }
            }
        }
        else
        {
            if (RunningTime <= 15)
            {
                RunningTime += Time.deltaTime;

            }

            speed -= 18 * Time.deltaTime;
            speed = Mathf.Clamp(speed, 8, 20);
        }

        if (!characterisrunning)
        {
            speed -= 29 * Time.deltaTime;
            speed = Mathf.Clamp(speed, 8, 20);
        }






        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
