using UnityEngine;

interface IInteractable1{
    public void VentChange();
}

public class ventcode : MonoBehaviour
{
    public GameObject player;
    public GameObject vent;
    public GameObject tovents;


    // Update is called once per frame
    void Update()
    {
       if (vent.activeSelf && Input.GetKey(KeyCode.X)) {
            vent.gameObject.SetActive(false);
            tovents.SetActive(false);
            player.SetActive(true);

            Vector3 newpos = vent.transform.position;
            player.transform.position = new Vector3(newpos.x, newpos.y, newpos.z);

       }

        Transform cam = vent.transform.Find("ventcam");

        if (Input.GetKeyDown(KeyCode.Q)){
            Ray r = new Ray(cam.position, cam.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo)){
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable1 interactObj)){
                    interactObj.VentChange();
            }
            }
        }

       
        
    }
}
