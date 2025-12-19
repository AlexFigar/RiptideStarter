using System;
using System.Collections.Generic;
using Riptide;
using Riptide.Utils;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager _instance;
    public static NetworkManager Instance { get { return _instance; } }

    public Server server;
    public Client client;


    //public Dictionary<ushort, GameObject> playerList;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);

        RiptideLogger.Initialize(Debug.Log, 
                                 Debug.Log, 
                                 Debug.LogWarning, 
                                 Debug.LogError, 
                                 false);
    }

    public void StartServer()
    {
        server = new();
        server.ClientConnected += ClientConnected;
        server.Start(7777, 4);
    }

    public void StartClient()
    {
        client = new();
        client.ClientConnected += ConnectedToServer;
        client.Connect("127.0.0.1:7777");
    }
    private void ClientConnected(object sender, ServerConnectedEventArgs e)
    {
        ServerBehaviour.SendDataToClient(e.Client.Id);
        Debug.Log("Client has connected");
    }
    private void ConnectedToServer(object sender, ClientConnectedEventArgs e)
    {
        ClientBehaviour.SendDataToServer(client.Id);
        Debug.Log("Connected to server");
    }

    public void FixedUpdate()
    {
        server?.Update();
        client?.Update();
    }
}