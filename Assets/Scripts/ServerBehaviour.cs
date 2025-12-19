using Riptide;
using UnityEngine;

public static class ServerBehaviour
{
    #region Messages
    public static void SendDataToClient(ushort clientID)
    {
        Debug.Log("Sending Data to "+clientID);
    }
    #endregion

    #region Message Handlers
    [MessageHandler((ushort)MessageID.HelloServerImAClient)]
    public static void ReceivedDataFromClient(ushort fromClientID, Message message)
    {
        Debug.Log("Client "+fromClientID+" says hello!");
    }
    #endregion
}