using Photon.Compression;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class BossZ : MonoBehaviour
{
    [SerializeField] protected Transform Target; // hedef al�ca�� obje
    [SerializeField] protected Animator anim;
    
    protected float health;
    protected int shield;
    protected float power;
    [SerializeField] protected float speed;
    public CharacterMovement character;

    [SerializeField] NavMeshAgent nmesh;

    protected int maxLook = 60;
    protected int minLook = 25;
    protected int minRockthrow = 15;
    protected int maxattack = 5;
    [SerializeField] protected bool rockThrow = false;
    protected float time = 0;
    protected float maxtime = 10;

    public Transform Character; // Karakterin transformu

    public Transform boss; // Boss'un transformu

    bool objectThrown = false;
    // Bu de�er, ray�n ne kadar uzun olaca��n� belirler.
    protected float rayLength = 100f;

    // Ray'�n �arpabilece�i katmanlar� filtrelemek i�in kullan�l�r.
    public LayerMask layerMask;

    public Transform rayOriginObject;
    public Transform targetObject;

    public GameObject objectToThrow; // F�rlat�lacak obje.
    float throwSpeed = 10f; // F�rlatma h�z�.
    public Transform throwOrigin;

    float upwardAdjustment = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(rockthrow());
    }

    // Update is called once per frame
    void Update()
    {

        BossMov();
        //CastRay();

    }


    public virtual void BossMov()
    {


        Vector3 TargetPos = Target.position;
        Vector3 BossPos = this.gameObject.transform.position;

        float distance = Vector3.Distance(TargetPos, BossPos); // hedef obje ile aras�ndaki mesafeyi hesapl�yor.

        //Debug.Log(distance);

        if (distance > minLook && distance < maxLook)
        {
            anim.SetBool("RockThrow", false);
            anim.SetBool("IsWalk", true);
            nmesh.destination = Target.transform.position;
            speed = 2.0f;

        }
        else if (distance >= minRockthrow && distance <= minLook && rockThrow)
        {
            objectToThrow.SetActive(true);
            anim.SetBool("RockThrow", true);
            anim.SetBool("IsWalk", false);
            nmesh.destination = this.transform.position;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
            {
                rockThrow = false;
            }
        }
        else if (distance >= minRockthrow && distance <= minLook && !rockThrow)
        {
            anim.SetBool("RockThrow", false);
            anim.SetBool("IsWalk", true);
            anim.SetBool("Hit", false);
            nmesh.destination = Target.transform.position;
        }
        else if (distance > maxattack && distance < minRockthrow)
        {
            anim.SetBool("RockThrow", false);
            anim.SetBool("IsWalk", true);
            anim.SetBool("Hit", false);
            nmesh.destination = Target.transform.position;
        }
        else if (distance <= maxattack && !character.isdeath)
        {
            anim.SetBool("RockThrow", false);
            anim.SetBool("Hit", true);
            nmesh.destination = this.transform.position;
        }
        else if (distance <= maxattack && character.isdeath)
        {

            anim.SetBool("RockThrow", false);
            anim.SetBool("Hit", false);
            anim.SetBool("IsWalk", true);
            nmesh.destination = Target.transform.position; //�ld�kten sonra hayatta kalan di�er oyuncuya gitmesi i�in
        }
        else
        {
            anim.SetBool("IsWalk", false);
            nmesh.destination = this.transform.position;
        }


        if (rayOriginObject != null && targetObject != null)
        {
            // Ray'�n ��k�� noktas�n� ve hedefe do�ru olan y�n�n� belirle.
            Vector3 rayOrigin = rayOriginObject.position;
            Vector3 toTarget = targetObject.position - rayOrigin;
            // Hedefe do�ru y�n vekt�r�ne hafif bir yukar� ayarlama ekleyin.
            Vector3 targetDirection = (toTarget + Vector3.up * upwardAdjustment).normalized;

            RaycastHit hit;
            // Raycast yap ve e�er bir �eyle �arp���rsa.
            if (Physics.Raycast(rayOrigin, targetDirection, out hit, rayLength, layerMask))
            {
                // E�er ray, hedef objeye �arparsa.
                if (hit.transform == targetObject)
                {
                    // Hedefe �arpt���n� konsola yazd�r.
                    //Debug.Log("Ray hit the target: " + hit.collider.gameObject.name);
                }

                // �arp��ma noktas�na kadar k�rm�z� bir �izgi �iz.
                Debug.DrawLine(rayOrigin, hit.point, Color.red);
            }
            else
            {
                // �arp��ma olmad���nda ray'�n tahmini yolunu ye�il olarak �iz.
                Debug.DrawRay(rayOrigin, targetDirection * rayLength, Color.green);
            }
        }
        if (!objectThrown && Vector3.Distance(Character.position, boss.position) < minLook && rockThrow)
        {
            // Objeyi f�rlat
            ThrowObject();
            objectThrown = true; // Obje f�rlat�ld���nda durumu g�ncelle
        }


    }

    void ThrowObject()
    {
        GameObject thrownObject = Instantiate(objectToThrow, Character.position, Quaternion.identity);

        Vector3 throwDirection = (Character.position - transform.position).normalized;
        thrownObject.GetComponent<Rigidbody>().velocity = throwDirection * throwSpeed;
    }




    IEnumerator rockthrow()
    {

        while (time <= maxtime)
        {
            yield return new WaitForSeconds(1);
            time++;
            Debug.Log(time);
            if (time == maxtime)
            {
                rockThrow = true;
                time = 0f;
            }
        }

    }
    
}
