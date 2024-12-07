using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{

    public TMP_InputField inputField;
    public TMP_Text outputText;
    public string capturedText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSubmit() { 
        capturedText = inputField.text;
        outputText.text = capturedText;
        Debug.Log("Captured Text: " + capturedText);

        inputField.text = "";
    }
}
