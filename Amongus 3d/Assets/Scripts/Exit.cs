using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject panel;
    public GameObject crosshairs;

    public void GetOut(){
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        crosshairs.SetActive(true);
    }
    
}