using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class tovent : MonoBehaviour, IInteractable1
{

    public GameObject ogcam;
    public GameObject toventog;

    public GameObject newcam;
    public GameObject toventnew;


    // Update is called once per frame
    public void VentChange()
    {
        ogcam.SetActive(false);
        toventog.SetActive(false);
        newcam.SetActive(true);
        toventnew.SetActive(true);
    }
}
