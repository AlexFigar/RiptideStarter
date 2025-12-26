using Riptide;
using UnityEngine;

public static class ServerBehaviour
{
    #region Messages
    public static void SendDataToClient(ushort clientID)
    {
        Message message = Message.Create(MessageSendMode.Reliable, MessageID.HelloClientImAServer);
        message.AddString("Hello Client!");
        NetworkManager.Instance.server.Send(message, clientID);
    }
    #endregion

    #region Message Handlers
    [MessageHandler((ushort)MessageID.HelloServerImAClient)]
    public static void ReceivedDataFromClient(ushort fromClientID, Message message)
    {
        Debug.Log("Client "+fromClientID+" says "+ message.GetString());
    }
    #endregion
}