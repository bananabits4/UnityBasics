using System;
using System.Runtime.CompilerServices;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    public Client Client {get; private set;}

    [SerializeField] private string ip;
    [SerializeField] private ushort port;
    [Space(0)]
    [SerializeField] private ushort tickdiversiontolerance = 1;

    private ushort _serverTick;
    public ushort ServerTick 
    {
        get => _serverTick;
        private set 
        {
            _serverTick = value;
            InterpolationTick = (ushort) (value - Timebetweeninterpolation); 
        }
    }

    private ushort _timebetweeninterpolation = 2;

    public ushort Timebetweeninterpolation{
        get => _timebetweeninterpolation;
        private set
        {
            _timebetweeninterpolation = value;
            InterpolationTick = (ushort)(ServerTick - value);

        }
    }

    public ushort InterpolationTick {get;private set;}

    private void Awake() {
        Singleton = this;
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.Disconnected += DidDisconnect;
        Client.ClientDisconnected += PlayerLeft;

        ServerTick = 2;

    }

    private void FixedUpdate() {
        Client.Tick();
        ServerTick++;
    }

    private void OnApplicationQuit() {
        Client.Disconnect();
    }

    public void Connect()
    {
        Client.Connect($"{ip}:{port}");

    }

    private void DidConnect(object sender, EventArgs e)
    {
        MainScreenManager.Singleton.SendName();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void PlayerLeft(object sender,ClientDisconnectedEventArgs e)
    {
        if (Player.list.TryGetValue(e.Id , out Player player))
        {
            Destroy(player.gameObject);
        }
    }

    private void FailedToConnect(object sender, EventArgs e)
    {
        MainScreenManager.Singleton.BackToMain();

    }

    private void DidDisconnect(object sender, EventArgs e)
    {
        MainScreenManager.Singleton.BackToMain();
        foreach (Player player in Player.list.Values)
        {
            Destroy(player.gameObject);
        }
    }

    private void SetTick(ushort servertick)
    {
        if (Math.Abs(ServerTick - servertick)> tickdiversiontolerance)
        {
            ServerTick = servertick;
        }
    }

    [MessageHandler((ushort)ServerToClientID.sync)]
    private static void Sync(Message message)
    {
        Singleton.SetTick(message.GetUShort());
    }
}

