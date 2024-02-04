using UnityEngine;

public class playermovement : MonoBehaviour
{
    public CharacterController player;

    public float speed = 15f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    public Transform groundcheck;
    public float grounddistance = 0.4f;
    public LayerMask groundmask;

    bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);

        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;
        player.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);

        
    }
}
