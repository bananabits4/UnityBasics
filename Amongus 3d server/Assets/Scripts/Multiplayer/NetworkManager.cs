using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using UnityEngine.Rendering;


public enum ServerToClientID : ushort{
    playerSpawned = 1,
    position,
    sync,
}

public enum ClientToServerID : ushort{
    name = 1,
    input,
}

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _Singleton;
    public static NetworkManager Singleton
    {
        get => _Singleton;
        private set
        {
            if(_Singleton == null)
                _Singleton = value;
            else if (_Singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying duplicate!!!");
                Destroy(value);
            }
        }
    }

    public Server Server {get; private set;}
    public ushort current_tick {get; private set;} = 0;

    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;

    private void Awake(){
        Singleton = this;
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.ClientDisconnected += PlayerLeft;

    }

    private void FixedUpdate() {
        Server.Tick();
        if (current_tick%250 == 0)
        {
            Send_Sync();
        }

        current_tick ++;
        }

    private void OnApplicationQuit() {
        Server.Stop();
    }

    private void PlayerLeft(object sender,ClientDisconnectedEventArgs e)
    {
        if (Player.list.TryGetValue(e.Id , out Player player))
        {
            Destroy(player.gameObject);
        }
    }

    private void Send_Sync()
    {
        Message message = Message.Create(MessageSendMode.unreliable,ServerToClientID.sync);
        message.AddUShort(current_tick);
        NetworkManager.Singleton.Server.SendToAll(message);
    }


    
}

