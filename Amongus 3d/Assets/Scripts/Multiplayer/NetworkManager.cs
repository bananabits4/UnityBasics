using System;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;


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
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying duplixcate!!!");
                Destroy(value);
            }
        }
    }

    public Client Client {get; private set;}

    [SerializeField] private string ip;
    [SerializeField] private ushort port;

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
    }

    private void FixedUpdate() {
        Client.Tick();
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

    private void FailedToConnect(object sender, EventArgs e)
    {
        MainScreenManager.Singleton.BackToMain();

    }

    private void DidDisconnect(object sender, EventArgs e)
    {
        MainScreenManager.Singleton.BackToMain();
    }
}

