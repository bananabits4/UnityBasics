using System;
using UnityEngine;
using System.Collections;

public class Vent : MonoBehaviour, IInteractable
{
    public GameObject player;//This is temporary 
    [SerializeField] GameObject vent;
    public int vent_cooldown = 30;

    public void Interact()
    {
        //This is supposed to run in server so run this logic in server and return the new position to the local player 

        //steps for multiplayer 
        //get q input 
        //check all the conditions below
        //instead of assigining playerfrom inspector you will need to get it from the function calling parent script 
        player_data data = player.GetComponent<player_data>();
        int vent_time = (int)(DateTime.Now - data.lastvented).TotalSeconds;
        if ( vent_time> vent_cooldown && data.is_imposter && data.player_inside_vent == false)
        {
            data.lastvented = DateTime.Now;
            //send a valid vent message to local player 
            //The local player will start playing vent sound ill handle that
            //The local player(including all crewmates) will run the below logic locally and the server will run it too (The message will be like vented in) then the local clients will run this code


            playermovement movement = player.GetComponent<playermovement>();
            movement.enabled = false;
            MeshRenderer playermesh = player.GetComponentInChildren<MeshRenderer>();
            playermesh.enabled = false;
            //Till here
            player.transform.position = vent.transform.position;
            StartCoroutine(venting());
            //After this the server will return return the new postition to the local player 

        }
        else if(data.player_inside_vent)
        {

            //The local player(including all crewmates) will run the below logic locally and the server will run it too and play a vent out soundsame upar wala par vented out ke saat 
            playermovement movement = player.GetComponent<playermovement>();
            movement.enabled = true;
            MeshRenderer playermesh = player.GetComponentInChildren<MeshRenderer>();
            playermesh.enabled = true;
            data.player_inside_vent = false;
            //Till here
            
        }
        else{
            //return the time left for venting to the local player 
        }

        IEnumerator venting(){yield return new WaitForSeconds(1);data.player_inside_vent = true;}
    }

}
