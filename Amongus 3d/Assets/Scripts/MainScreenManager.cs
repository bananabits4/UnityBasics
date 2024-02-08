using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour
{
    private static MainScreenManager _Singleton;
    public static MainScreenManager Singleton
    {
        get => _Singleton;
        private set
        {
            if(_Singleton == null)
                _Singleton = value;
            else if (_Singleton != value)
            {
                Debug.Log($"{nameof(MainScreenManager)} instance already exists, destroying duplixcate!!!");
                Destroy(value);
            }
        }
    }

    [Header("Connect")]
    [SerializeField] private GameObject connectUI;
    [SerializeField] private TMP_InputField usernameField;

    private void Awake() {
        Singleton = this;
    }

    public void ConnectClicked()
    {
        usernameField.interactable = false;
        connectUI.SetActive(false);

        NetworkManager.Singleton.Connect();

        
    }

    public void BackToMain()
    {
        usernameField.interactable = true;
        connectUI.SetActive(true);
    }

    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.reliable, (ushort)ClientToServerID.name);
        message.AddString(usernameField.text);
        NetworkManager.Singleton.Client.Send(message);
    }
}
