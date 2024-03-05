using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    enum MyEnum
    {
     
        

    }

    [SerializeField] CharacterController controller;

    [SerializeField] float speed = 2f;

    [SerializeField] float gravity = -19f;

    [SerializeField] float jumpHeight = 1f;

    [SerializeField] Transform groundCheck;

    [SerializeField] float groundDistance = 4f;

    [SerializeField] LayerMask groundMask;

    [SerializeField] float RunningTime = 15; //koþma süresi

    public bool isdeath = false;

    [SerializeField] bool ChaLeansLeftOrRight = false;

    [SerializeField] TextMeshPro tmprotext;


    [SerializeField] AudioClip rapidSound;

    [SerializeField] Image shieldImage;
    [SerializeField] Image HealthImage;

    public float health = 100;
    public float shield = 50;

     Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isdeath)
        {
            animator.SetTrigger("IsDeath");
        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 DirectionVector = new Vector3(x, 0, z);

            controller.Move(DirectionVector * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(DirectionVector * speed * 3 * Time.deltaTime);

            }

        }





    }

    private void OnTriggerEnter(Collider boss)
    {   
          if (boss.gameObject.CompareTag("hands"))
          {

            if (shield > 0)
            {
                DamageHealth1();
                Damageshield1();
                shieldImage.fillAmount = shield / 50;
                HealthImage.fillAmount = health / 100;
                
            }
            else
            {
                shield = 0;
                NoneShieldDamageHealth1();
                HealthImage.fillAmount = health / 100;
               
            }
            if (health <= 0)
            {
                health = 0;
                isdeath = true;
            }

            }
          else if (boss.gameObject.CompareTag("HandsNormal"))
          {

               if (shield > 0)
               {
                DamageHealth2();
                Damageshield2();
                shieldImage.fillAmount = shield / 50;
                HealthImage.fillAmount = health / 100;

               }
               else
               {
                shield = 0;
                NoneShieldDamageHealth2();
                HealthImage.fillAmount = health / 100;
 
               }
               if (health <= 0)
               {
                health = 0;
                isdeath = true;
               }

         }
          else if (boss.gameObject.CompareTag("handsanormal"))
        {

            if (shield > 0)
            {
                DamageHealth3();
                Damageshield3();
                shieldImage.fillAmount = shield / 50;
                HealthImage.fillAmount = health / 100;

            }
            else
            {
                shield = 0;
                NoneShieldDamageHealth3();
                HealthImage.fillAmount = health / 100;

            }
            if (health <= 0)
            {
                health = 0;
                isdeath = true;
            }

        }


    }


    public void Damageshield1()
    {
      int shieldamage = UnityEngine.Random.Range(3, 6);
        shield -= shieldamage;
    }
    public void DamageHealth1()
    {
        int healthdamage = UnityEngine.Random.Range(4, 7);
        health -= healthdamage;
    }
    public void NoneShieldDamageHealth1()
    {
        int healthdamage = UnityEngine.Random.Range(4, 7);
        health -= healthdamage;
    }

    public void Damageshield2()
    {
        int shieldamage = UnityEngine.Random.Range(5, 8);
        shield -= shieldamage;
    }
    public void DamageHealth2()
    {
        int healthdamage = UnityEngine.Random.Range(4, 8);
        health -= healthdamage;
    }
    public void NoneShieldDamageHealth2()
    {
        int healthdamage = UnityEngine.Random.Range(6, 10);
        health -= healthdamage;
    } 
    public void Damageshield3()
    {
        int shieldamage = UnityEngine.Random.Range(7, 9);
        shield -= shieldamage;
    }
    public void DamageHealth3()
    {
        int healthdamage = UnityEngine.Random.Range(5, 9);
        health -= healthdamage;
    }
    public void NoneShieldDamageHealth3()
    {
        int healthdamage = UnityEngine.Random.Range(7, 12);
        health -= healthdamage;
    }
}
