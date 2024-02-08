using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour, IInteractable
{   
    public GameObject task1;

    public playerlook look;
    public playermovement move;

    public void Interact(string type = ""){
        Cursor.lockState = CursorLockMode.None;
        task1.SetActive(true);
        look.enabled =  false;
        move.enabled = false;

    }

    public void close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        task1.SetActive(false);
        look.enabled =  true;
        move.enabled = true;
    }
}
