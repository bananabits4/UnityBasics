using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour, IInteractable
{
    public GameObject player;
    public GameObject ventcam;
    public Transform pos;

    public Vector3 offset;
    public void Interact()
    {
        Debug.Log("you reached the place");
        player.SetActive(false);
        
        ventcam.gameObject.SetActive(true);
    }
}
