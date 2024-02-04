using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour, IInteractable
{   
    public GameObject task1;
    public GameObject crosshairs;

    public void Interact(){
        Cursor.lockState = CursorLockMode.None;
        task1.SetActive(true);
        crosshairs.SetActive(false);
    }
}
