using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager _instance;
    public static NetworkManager Instance { get { return _instance; } }

    public static Dictionary<ushort, GameObject> playerList = new();

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}