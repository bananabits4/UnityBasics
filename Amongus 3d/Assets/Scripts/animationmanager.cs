using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationmanager : MonoBehaviour
{
    private Animator animation_controller;
    // Start is called before the first frame update
    void Start()
    {
        animation_controller = gameObject.GetComponent<Animator>();
        StartCoroutine(setvalue());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator setvalue()
    {
        while (true)
        {
            
        yield return new WaitForSeconds(15);
        animation_controller.SetInteger("idlestate",Random.Range(1,101));
        }
    }
}
