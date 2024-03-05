using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TwoDmansionalAnimationStateController : MonoBehaviour
{
    Animator animator;

    float velocityX = 0.0f;
    float velocityZ = 0.0f;

    //PERFORMANS ���N
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

            //h�z� azaltma i�lemleri

            if (!ForwardPressed && velocityZ > 0.0f)
            {
                velocityZ -= Time.deltaTime * deceleration;
            }

            // e�er sol tu�a yani A ya bas�lmazsa h�z�n� (-) den 0 a do�ru artt�r�r ve (-) de�eri 0 a �eker

            if (!LeftPressed && velocityX < 0.0f)
            {
                velocityX += Time.deltaTime * deceleration;
            }

            // e�er sa� tu�a yani D ye bas�lmazsa h�z�n� azalt�r ve (+) de�eri 0 a �eker

            if (!RightPressed && velocityX > 0.0f)
            {
                velocityX -= Time.deltaTime * deceleration;
            }
      

    }

    void lockOrResetVelocity(bool ForwardPressed, bool LeftPressed, bool RightPressed, bool RunPressed, float currentMaxVelocity)
    {
        //Z deki h�z�n� resetleme

        if (!ForwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }



        // X deki h�z�n� resetleme

        if ((!LeftPressed && !RightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f)))
        {
            velocityX = 0.0f; // e�er hem a ya hemde d ye bas�lm�yorsa de�er -0.05 ile 0.05 aras�na gelirse 0 a �eker ve karakteri yanlarda hareket ettirmez
        }


        //h�z� s�n�rlamak i�in

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


        // klavyedeki tu� girdilerini almak
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

        float currentMaxVelocity = RunPressed ? maxRunVelocity : maxWalkVelocity; //e�er shifte bas��p tutarsa true olarak d�ner ve anl�k velocityi 1. olan a

          changeVelocity(ForwardPressed, LeftPressed, BackWardPressed, RightPressed, currentMaxVelocity);
          lockOrResetVelocity(ForwardPressed, LeftPressed, RightPressed, RunPressed, currentMaxVelocity);
        
       

        if (GunisActive) //true ise girecek
        {
            animator.SetBool("GunIsActive", GunisActive); // ve true yap�cak
        }
        else // de�ilse 
        {
            animator.SetBool("GunIsActive", GunisActive); // false d�nd�r�p silah� sakl�yacak
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




        if (isReoladPressed && ForwardPressed && GunisActive) // �arj�r de�i�tiriyorken ayn� zamanda ileride gidiyorsa ileri gitme animasyonu ile beraber �al���cak
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
