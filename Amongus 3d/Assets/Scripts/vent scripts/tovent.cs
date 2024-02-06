using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class tovent : MonoBehaviour, IInteractable
{
    public Transform finalvent;
    public GameObject player;


    // Update is called once per frame
    public void Interact()
    {
        GameObject toventog = transform.parent.gameObject;
        GameObject toventnew = finalvent.transform.Find("fromvent").gameObject;
        player.transform.position = finalvent.position;
        toventog.SetActive(false);
        toventnew.SetActive(true);
    }
}
