using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;


public enum ServerToClientID : ushort{
    playerSpawned = 1,
    position,
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
        }

    private void OnApplicationQuit() {
        Server.Stop();
    }

    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        Destroy(Player.list[e.Id].gameObject);
    }

    
}

