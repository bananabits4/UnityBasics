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
    [SerializeField]
    AudioSource playerAudio;

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

        // Check if the player is moving and play or stop the sound accordingly
        if (move != Vector3.zero && isGrounded) // If the player is moving
        {
            if (!playerAudio.isPlaying) // If the sound is not playing
            {
                playerAudio.Play(); // Play the sound
            }
        }
        else // If the player is not moving
        {
            if (playerAudio.isPlaying) // If the sound is playing
            {
                playerAudio.Stop(); // Stop the sound
            }
        }
        player.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);

        
    }
}
