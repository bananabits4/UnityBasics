using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private static GameLogic _Singleton;
    public static GameLogic Singleton
    {
        get => _Singleton;
        private set
        {
            if(_Singleton == null)
                _Singleton = value;
            else if (_Singleton != value)
            {
                Debug.Log($"{nameof(GameLogic)} instance already exists, destroying duplixcate!!!");
                Destroy(value);
            }
        }
    }

    public GameObject PlayerPrefabs => playerPrefab;

    [Header("prefabs")]
    [SerializeField] private GameObject playerPrefab;

    private void Awake() {
        Singleton = this;

    }
}
