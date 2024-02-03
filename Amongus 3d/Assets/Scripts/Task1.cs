using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour, IInteractable
{   
    public GameObject task1;

    public void Interact(){
        task1.SetActive(true);
    }
}
