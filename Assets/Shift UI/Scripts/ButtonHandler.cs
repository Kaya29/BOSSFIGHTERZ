using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class ButtonHandler : MonoBehaviour
{
    [SerializeField] string ButtonText;
    [SerializeField] float fontSize = 25f;
    [SerializeField] TextMeshProUGUI[] tmps;
    // Start is called before the first frame update
    void Update()
    {
        tmps = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var t in tmps) 
        {
            t.text = ButtonText;
            t.fontSize = fontSize;
        }
    }

}
