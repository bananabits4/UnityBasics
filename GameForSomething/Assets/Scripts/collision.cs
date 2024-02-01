
using UnityEngine;

public class collision : MonoBehaviour
{
    public GameObject gameover;
    public MovementCode collide;
    
    void OnCollisionEnter (Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            collide.enabled = false;
            gameover.SetActive(true);

        }
    }

}
