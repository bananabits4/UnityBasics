using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;
using System.Runtime.ExceptionServices;

public class playercontroller : MonoBehaviour
{
    public Transform camers;
    private bool[] inputs;

    void Start()
    {
        inputs = new bool[5];
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            inputs[0] = true;
        }
        if(Input.GetKey(KeyCode.S))
        {
            inputs[1] = true;
        }
        if(Input.GetKey(KeyCode.A))
        {
            inputs[2] = true;
        }
        if(Input.GetKey(KeyCode.D))
        {
            inputs[3] = true;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            inputs[4] = true;
        }
    }

    void SendInput()
    {
        Message message = Message.Create(MessageSendMode.unreliable,ClientToServerID.input);
        message.AddBools(inputs,false);
        message.AddVector3(camers.transform.forward);
        NetworkManager.Singleton.Client.Send(message);
    }
    void FixedUpdate()
    {
        SendInput();
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i] = false;
        }
    }
}
