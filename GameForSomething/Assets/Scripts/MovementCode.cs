
using UnityEngine;

public class MovementCode : MonoBehaviour
{

    public Rigidbody rb;
    public float Fwdforce = 250f;
    public float sideforce = 100f;

    public GameObject gameover;


    void FixedUpdate ()
    {
        rb.AddForce(0, 0, Fwdforce *Time.deltaTime);

        if ( Input.GetKey ("d") )
        {
            rb.AddForce(sideforce*Time.deltaTime, 0, 0);
        }
        if ( Input.GetKey ("a") )
        {
            rb.AddForce(-sideforce*Time.deltaTime, 0, 0);
        }
        if (rb.position.y < -1f)
        {
            gameover.SetActive(true);
            
        }
    }
}
