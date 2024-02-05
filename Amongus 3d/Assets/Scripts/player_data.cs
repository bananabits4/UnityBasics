using System;
using UnityEngine;
using System.Collections;


public class player_data : MonoBehaviour
{
    public bool is_imposter ;
    public DateTime lastvented;

    public bool player_inside_vent ;

    void Awake()
    {
        lastvented = DateTime.Now;
    }

  
}
