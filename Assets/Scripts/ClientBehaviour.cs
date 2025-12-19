using Riptide;
using UnityEngine;

public static class ClientBehaviour
{
    #region Messages
    public static void SendDataToServer(ushort clientID)
    {
        Debug.Log("Sending data to server");
    }
    #endregion

    #region Message Handlers
    //Handles the servers message Message ID 1
    [MessageHandler((ushort)MessageID.HelloClientImAServer)]
    public static void ReceivedDataFromServer(Message message)
    {
        Debug.Log("Server says hello!");
    }
    #endregion
}