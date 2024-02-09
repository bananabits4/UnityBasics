using RiptideNetworking;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();

    public ushort Id { get; private set; }
    public bool IsLocal { get; private set; }

    private string username;

    [SerializeField] private Transform camtransform;

    [SerializeField] private Interpolator interpolator;

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    public void move(ushort tick,bool isTeleport,Vector3 newposition,Vector3 forward)
    {
        interpolator.NewUpdate(tick,isTeleport,newposition);

        if (!IsLocal)
        {
            camtransform.forward = forward;
        }
    }

    public static void Spawn(ushort id, string username, Vector3 position)
    {
        Player player;
        if (id == NetworkManager.Singleton.Client.Id)
        {
            player = Instantiate(GameLogic.Singleton.LocalPlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
            player.IsLocal = true;
        }
        else
        {
            player = Instantiate(GameLogic.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
            player.IsLocal = false;
        }

        player.name = $"Player {id} (username)";
        player.Id = id;
        player.username = username;

        list.Add(id, player);
    }

    [MessageHandler((ushort)ServerToClientID.playerSpawned)]
    private static void SpawnPlayer(Message message)
    {
        Spawn(message.GetUShort(), message.GetString(), message.GetVector3());
    }
    [MessageHandler((ushort)ServerToClientID.position)]
    private static void PlayerMovement(Message message)
    {
        if(list.TryGetValue(message.GetUShort(),out Player player))
            player.move(message.GetUShort(),message.GetBool(),message.GetVector3(),message.GetVector3());
    }
}
