using System;
using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using Unity.VisualScripting;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public CharacterController controller;
    public Player player;
    public Transform camproxy;

    public float gravity;

    public float speed;
    public float jump_height ;

    private bool[] inputs;
    private float yvelocity;

    private float gravityaccn;
    private float movespeed;
    private float jumpspeed;
    private bool didteleport;



    private void OnValidate()
    {
        if (controller ==null)
        {
            controller = GetComponent<CharacterController>();
        }
        if (player == null)
        {
            player = GetComponent<Player>();
        }
        Initilize();
    }

    void Initilize()
    {
        gravityaccn = gravity*Time.fixedDeltaTime*Time.fixedDeltaTime;
        movespeed = speed+Time.fixedDeltaTime;
        jumpspeed =Mathf.Sqrt(jump_height* -2f* gravityaccn);

    }

    // Start is called before the first frame update
    void Start()
    {
        inputs = new bool[5];
        Initilize();
    }

    private void FixedUpdate()
    {
        Vector2 inputdirection = Vector2.zero;
        if (inputs[0])
        {
            inputdirection.y +=1;
        }
        if (inputs[1])
        {
            inputdirection.y -=1;
        }  
        if (inputs[2])
        {
            inputdirection.x +=1;
        } 
        if (inputs[3])
        {
            inputdirection.x -=1;
        } 
        move(inputdirection,inputs[4]);
    }

    private void move(Vector2 inputdirection,bool jumping)
    {
        Vector3 movedirection = Vector3.Normalize(camproxy.right*inputdirection.x+Vector3.Normalize(flattenvector3(camproxy.forward))*inputdirection.y);
        movedirection*=movespeed;

        if (controller.isGrounded)
        {
            yvelocity = 0 ;
            if (jumping)
            {
                yvelocity = jumpspeed;
            }
        }
        yvelocity += gravityaccn;
        movedirection.y =  yvelocity;
        controller.Move(movedirection);

        SendMovement();

    }

    private Vector3 flattenvector3(Vector3 vector3)
    {
        vector3.y = 0 ;
        return vector3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setinput(bool[] inputs,Vector3 forward)
    {
        this.inputs = inputs;
        camproxy.forward = forward;
    }

    private void SendMovement()
    {
        if (NetworkManager.Singleton.current_tick % 2 != 0)
            return;
        Message message = Message.Create(MessageSendMode.unreliable,ServerToClientID.position);
        message.AddUShort(player.Id);
        message.AddUShort(NetworkManager.Singleton.current_tick);
        message.AddBool(didteleport);
        message.AddVector3(transform.position);
        message.AddVector3(camproxy.forward);
        NetworkManager.Singleton.Server.SendToAll(message);
    }

}
