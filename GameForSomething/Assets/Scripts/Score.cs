using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoretext;

    
    void Update()
    {
        scoretext.text = player.position.z.ToString("0");
    }
}
