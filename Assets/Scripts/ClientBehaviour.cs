using Riptide;
using UnityEngine;

public static class ClientBehaviour
{
    #region Messages
    public static void SendDataToServer()
    {
        Message message = Message.Create( MessageSendMode.Reliable, MessageID.HelloServerImAClient);
        message.AddString("Hello Server!");
        NetworkManager.Instance.client.Send(message);
    }
    #endregion

    #region Message Handlers
    //Handles the servers message Message ID 1
    [MessageHandler((ushort)MessageID.HelloClientImAServer)]
    public static void ReceivedDataFromServer(Message message)
    {
        Debug.Log("Server says "+ message.GetString());
    }
    #endregion
}