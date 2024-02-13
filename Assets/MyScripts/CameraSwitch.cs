using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] GameObject ThirdPerson, FirstPerson; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ThirdPerson.SetActive(!ThirdPerson.activeSelf);
            FirstPerson.SetActive(!FirstPerson.activeSelf);
        }
    }
}
