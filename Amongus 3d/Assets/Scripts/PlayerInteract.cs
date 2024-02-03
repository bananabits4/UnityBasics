using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable{
    public void Interact();
}


public class PlayerInteract : MonoBehaviour
{
    public Transform cam;
    public float interactrange;



    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            Ray r = new Ray(cam.position, cam.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactrange)){
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)){
                    interactObj.Interact();
            }
            }
        }
    }
}
