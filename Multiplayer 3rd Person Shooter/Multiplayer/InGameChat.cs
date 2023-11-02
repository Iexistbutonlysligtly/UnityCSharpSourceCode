using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using ExitGames.Client.Photon;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class InGameChat : MonoBehaviour, IChatClientListener
{

    public string userName;
    public ChatClient ChatClient;

    public InputField InputField;
    public Text ChatContent;

    public Transform chatPannel;



    public void ShowChat()
    {

        chatPannel.gameObject.SetActive(true);

    }


    public void CloseChat()
    {

        chatPannel.gameObject.SetActive(false);

    }





    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("Chat - " + level + " - " + message);
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("Chat - OnChatStateChange" + state);
    }

    public void OnConnected()
    {
        Debug.Log("Chat - User:" + userName + " has connected");
        ChatClient.Subscribe(PhotonNetwork.CurrentRoom.Name, creationOptions: new ChannelCreationOptions() { PublishSubscribers = true });
    }

    public void OnDisconnected()
    {
        Debug.Log("Chat - User:" + userName + " has disconnected");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        ChatChannel currentChat;

        if (ChatClient.TryGetChannel(PhotonNetwork.CurrentRoom.Name,out currentChat))
        {
            ChatContent.text = currentChat.ToStringMessages();
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
      for (int i = 0; i < channels.Length; i++)
        {

            if (results[i])
            {

                Debug.Log("Chat - Subscribe to " + channels[i] + "channel");
                //ChatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, " has joined the chat");
            }

        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }



    public void SetMessage()
    {
        if (InputField.text == "")
            return;


        ChatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, InputField.text);
        InputField.text = "";

    }



    // Start is called before the first frame update
    void Start()
    {
        ChatClient = new ChatClient(this);




        //This Althenticates a Photon Connection and Deterimes If It Allowed To Connect To The Chat via The Player's Nickname
        var authenticationValues = new Photon.Chat.AuthenticationValues(PhotonNetwork.LocalPlayer.NickName);

        //Gives The Chat Class The Client Name
        userName = PhotonNetwork.LocalPlayer.NickName;

        //Begins The Connectin To The hoton Chat.The Gives It The AppID,AppVertion and The Althenticates Values
        ChatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0", authenticationValues);
    }

    // Update is called once per frame
    void Update()
    {

        ChatClient.Service();
        
    }
}
