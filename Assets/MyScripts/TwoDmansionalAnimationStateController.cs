using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TwoDmansionalAnimationStateController : MonoBehaviour
{
    Animator animator;

    float velocityX = 0.0f;
    float velocityZ = 0.0f;

    //PERFORMANS ÝÇÝN
    int VelocityZMASH;
    int VelocityXMASH;

    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2.0f;

    [SerializeField] bool GunisActive = false;
    [SerializeField] bool IsCrouching;
    [SerializeField] bool IsIdleCrouching;
    [SerializeField] bool IsRooling;
    [SerializeField] bool IsReloading;




    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityXMASH = Animator.StringToHash("Velocity X");
        VelocityZMASH = Animator.StringToHash("Velocity Z");
    }

    void changeVelocity(bool ForwardPressed, bool LeftPressed,bool BackWardPressed, bool RightPressed, float currentMaxVelocity)
    {

            if (ForwardPressed && velocityZ < currentMaxVelocity && !GunisActive) // silah yoksa
            {
                velocityZ += Time.deltaTime * acceleration;
            }
            if (ForwardPressed && velocityZ < currentMaxVelocity && GunisActive) // silah varsa
            {
                velocityZ += Time.deltaTime * acceleration;
            }
            //if (BackWardPressed && (velocityZ > -0.5f && velocityZ <= 0.0f))
            //{
            //    velocityZ += Time.deltaTime * acceleration;
            //}


            if (LeftPressed && velocityX > -currentMaxVelocity && !GunisActive)
            {
                velocityX -= Time.deltaTime * acceleration;
            }
            if (LeftPressed && velocityX > -currentMaxVelocity && GunisActive)
            {
                velocityX -= Time.deltaTime * acceleration;
            }

            if (RightPressed && velocityX < currentMaxVelocity && !GunisActive)
            {
                velocityX += Time.deltaTime * acceleration;
            }
            if (RightPressed && velocityX < currentMaxVelocity && GunisActive)
            {
                velocityX += Time.deltaTime * acceleration;
            }

            //hýzý azaltma iþlemleri

            if (!ForwardPressed && velocityZ > 0.0f)
            {
                velocityZ -= Time.deltaTime * deceleration;
            }

            // eðer sol tuþa yani A ya basýlmazsa hýzýný (-) den 0 a doðru arttýrýr ve (-) deðeri 0 a çeker

            if (!LeftPressed && velocityX < 0.0f)
            {
                velocityX += Time.deltaTime * deceleration;
            }

            // eðer sað tuþa yani D ye basýlmazsa hýzýný azaltýr ve (+) deðeri 0 a çeker

            if (!RightPressed && velocityX > 0.0f)
            {
                velocityX -= Time.deltaTime * deceleration;
            }
      

    }

    void lockOrResetVelocity(bool ForwardPressed, bool LeftPressed, bool RightPressed, bool RunPressed, float currentMaxVelocity)
    {
        //Z deki hýzýný resetleme

        if (!ForwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }



        // X deki hýzýný resetleme

        if ((!LeftPressed && !RightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f)))
        {
            velocityX = 0.0f; // eðer hem a ya hemde d ye basýlmýyorsa deðer -0.05 ile 0.05 arasýna gelirse 0 a çeker ve karakteri yanlarda hareket ettirmez
        }


        //hýzý sýnýrlamak için

        if (ForwardPressed && RunPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if (ForwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;

            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (ForwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05))
        {
            velocityZ = currentMaxVelocity;
        }

        if (LeftPressed && RunPressed && velocityX < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        else if (LeftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;

            if (velocityX < currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        else if (LeftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05))
        {
            velocityX = -currentMaxVelocity;
        }


        if (RightPressed && RunPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if (RightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;

            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity - 0.05))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (RightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity + 0.05))
        {
            velocityZ = currentMaxVelocity;
        }


    }

    void Update()
    {       


        // klavyedeki tuþ girdilerini almak
        bool ForwardPressed = Input.GetKey(KeyCode.W);
        bool BackWardPressed = Input.GetKey(KeyCode.S);
        bool RightPressed = Input.GetKey(KeyCode.D); 
        bool LeftPressed = Input.GetKey(KeyCode.A);
        bool RunPressed = Input.GetKey(KeyCode.LeftShift);
        bool CrouchPressed = Input.GetKey(KeyCode.LeftControl);
        bool isRoolingPressed = Input.GetKeyDown(KeyCode.C);
        bool isReoladPressed = Input.GetKeyDown(KeyCode.R);
        


        if (CrouchPressed && GunisActive)
        {
            IsCrouching = true;
            IsIdleCrouching = false;
        }
        else if (CrouchPressed && !GunisActive)
        {
            IsCrouching = false;
            IsIdleCrouching = true;
        }

        float currentMaxVelocity = RunPressed ? maxRunVelocity : maxWalkVelocity; //eðer shifte basýðp tutarsa true olarak döner ve anlýk velocityi 1. olan a

          changeVelocity(ForwardPressed, LeftPressed, BackWardPressed, RightPressed, currentMaxVelocity);
          lockOrResetVelocity(ForwardPressed, LeftPressed, RightPressed, RunPressed, currentMaxVelocity);
        
       

        if (GunisActive) //true ise girecek
        {
            animator.SetBool("GunIsActive", GunisActive); // ve true yapýcak
        }
        else // deðilse 
        {
            animator.SetBool("GunIsActive", GunisActive); // false döndürüp silahý saklýyacak
        }


        if (CrouchPressed)
        {
            if (IsCrouching)
            {
                animator.SetBool("IsCrouching", IsCrouching);
            }
            if (IsIdleCrouching)
            {
                animator.SetBool("IsCrouchingIdle", IsIdleCrouching);
            }
           
        }
        else
        {
            IsCrouching = false;
            IsIdleCrouching = false;
            animator.SetBool("IsCrouchingIdle", IsIdleCrouching);
            animator.SetBool("IsCrouching", IsCrouching);

        }

        if (isRoolingPressed)
        {
            animator.SetBool("Rooling", isRoolingPressed);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sprinting Forward Roll"))
        {
            animator.SetBool("Rooling", isRoolingPressed);
        }




        if (isReoladPressed && ForwardPressed && GunisActive) // þarjör deðiþtiriyorken ayný zamanda ileride gidiyorsa ileri gitme animasyonu ile beraber çalýþýcak
        {
            animator.SetTrigger("ForwardReolading");
            IsReloading = true;

        }
        if (!ForwardPressed && IsReloading)
        {
            animator.SetBool("IdleRToWalkReload",false);
        }
        else if (isReoladPressed && GunisActive && !ForwardPressed)
        {
            animator.SetBool("IdleRToWalkReload",true);
            IsReloading = true;
        }
        if (ForwardPressed && IsReloading)
        {
            animator.SetBool("IdleRToWalkReload", true);

            Debug.Log("dasdasdad");
        }


        animator.SetFloat(VelocityXMASH, velocityX);

        animator.SetFloat(VelocityZMASH, velocityZ);
    }
}
