using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject panel;

    public void GetOut(){
        panel.SetActive(false);
    }
    
}