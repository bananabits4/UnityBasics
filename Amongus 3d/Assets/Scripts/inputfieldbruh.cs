using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inputfieldbruh : MonoBehaviour
{


    private TMP_InputField inputField; 
    // Reference to the Input Field component
    public iamahuman script;
    private void Start()
    {
        // Get the Input Field component
        inputField = GetComponent<TMP_InputField>();

        // Add a listener to handle value changes
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    private void OnInputFieldValueChanged(string newValue)
    {
        // Handle the input field value (e.g., pass it to another function or update a variable)
        script.validate_info(newValue);
        
    }
}

