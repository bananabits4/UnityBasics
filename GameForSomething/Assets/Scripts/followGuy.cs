
using UnityEngine;

public class followGuy : MonoBehaviour
{

    public Transform guy;
    public Vector3 offset;


    // Update is called once per frame
    void Update()
    {
        transform.position = guy.position + offset;
    }
}
