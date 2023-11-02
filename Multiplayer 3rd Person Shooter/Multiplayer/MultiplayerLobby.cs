using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;


using PlayFab;
using PlayFab.ClientModels;

public class MultiplayerLobby : MonoBehaviourPunCallbacks
{
    public Transform LoginPannel;
    public Transform SlectionPannel;
    public Transform CreateRoomPannel;
    public Transform InsideRoomPannel;
    public Transform ListRoomPannel;
    public Transform chatPannel;

    public TMP_InputField RoomName;
    public TMP_InputField PlayerNameInput;

    string PlayerName;

    public GameObject TextPrefab;
    public Transform InsideRoomPlayerList;

    public Transform listRoomPanel;
    public GameObject roomEntryPrefab;
    public Transform listRoomPanelContent;


    public Transform PlayerAmmoutErrorMessage;

    Dictionary<string, RoomInfo> cachedRoomList;

    public GameObject StartButton;



    public Chat Chat;

    bool instantiatedAvatars;




    private void Start()
    {
        PlayerNameInput.text = PlayerName = string.Format("Player {0}", Random.Range(1, 1000000));

        cachedRoomList = new Dictionary<string, RoomInfo>();

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void StartGameClicked()
    {


        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (!(playerCount >= 2))
        {
            Debug.Log("You Godda Wait For A Second Player Bro");
            
            
            if (!PlayerAmmoutErrorMessage.gameObject.activeSelf)
            {

                PlayerAmmoutErrorMessage.gameObject.SetActive(true);

                Invoke("TurnOffMessage", 3);

            }
           
        }


  
     

        else
        {

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel("Multiplayer");


        }

        

        


      

        
    }

    void TurnOffMessage()
    {
        PlayerAmmoutErrorMessage.gameObject.SetActive(false);
    }

    public void LoginClicked()
    {


        if (PlayerNameInput.text.Trim() != "")
        {

            PhotonNetwork.LocalPlayer.NickName = PlayerName = PlayerNameInput.text;
            //Trys To the Photon Master Server using Settings stored in the PhotonServerSettings GameObject
            PhotonNetwork.ConnectUsingSettings();

            UpdatePlayfabUsername(PlayerName);



        }

        else
        {

            Debug.Log("Player Name is Invalid");
        }


    }


    void UpdatePlayfabUsername(string username)
    {


        UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest
        {

            DisplayName = username,

        };


        PlayFabClientAPI.UpdateUserTitleDisplayName(request, PlayFabUpdateUserTitleDisplayNameResult, PlayFabUpdateUserTitleDisplayNameError);

    }



    void PlayFabUpdateUserTitleDisplayNameResult(UpdateUserTitleDisplayNameResult updateUserTitleDisplayNameResult)

    {

        Debug.Log("PlayFab — UserTitleDisplayName updated.");

    }



    void PlayFabUpdateUserTitleDisplayNameError(PlayFabError updateUserTitleDisplayNameError)

    {

        Debug.Log("P1ayFab - Error occurred while updating UserTitleDisplayName: " + updateUserTitleDisplayNameError.ErrorMessage);
    }





    public void DisconnectClicked()
    {

        PhotonNetwork.Disconnect();
    }

    public void LeaveRoom()
    {

        PhotonNetwork.LeaveRoom();

    }

    public override void OnLeftRoom()
    {
        //Leaves The Chat When We Leave The Game Room
        Chat.ChatClient.Disconnect();

        Debug.Log("Room Has Been Jaoined");
        ActivatePannel("CreateRoom");

        DestroyChildren(InsideRoomPlayerList);
    }


    public override void OnConnectedToMaster()
    {




        Debug.Log("We're on the server babby!!!!!!!!");
        ActivatePannel("Selection");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {

        Debug.Log("We Outta this lace baby!!!!! Disconnecting");
        ActivatePannel("Login");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("We Made Da Room Babyyyyyy!!!!!!");
    }

    public override void OnCreateRoomFailed(short returncode, string message)
    {

        Debug.Log("Room Creation Was A Faliure");
    }

    public override void OnJoinedRoom()
    {

        //This Althenticates a Photon Connection and Deterimes If It Allowed To Connect To The Chat via The Player's Nickname
        var authenticationValues = new Photon.Chat.AuthenticationValues(PhotonNetwork.LocalPlayer.NickName);

        //Gives The Chat Class The Client Name
        Chat.userName = PhotonNetwork.LocalPlayer.NickName;

        //Begins The Connectin To The hoton Chat.The Gives It The AppID,AppVertion and The Althenticates Values
        Chat.ChatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0", authenticationValues);



        Debug.Log("Joing The Room Was Sucsessful");
        ActivatePannel("InsideRoom");
        StartButton.SetActive(PhotonNetwork.IsMasterClient);
        foreach (var player in PhotonNetwork.PlayerList)
        {
            var PlayerListEntry = Instantiate(TextPrefab, InsideRoomPlayerList);
            PlayerListEntry.GetComponent<Text>().text = player.NickName;
            PlayerListEntry.name = player.NickName;
        }





        if (PhotonNetwork.CountOfPlayers == 2)
        {

            instantiatedAvatars = true;


        }



    }



    public void ActivatePannel(string pannelName)
    {
        LoginPannel.gameObject.SetActive(false);
        SlectionPannel.gameObject.SetActive(false);
        CreateRoomPannel.gameObject.SetActive(false);
        InsideRoomPannel.gameObject.SetActive(false);
        ListRoomPannel.gameObject.SetActive(false);
        chatPannel.gameObject.SetActive(false);

        if (pannelName == LoginPannel.gameObject.name) LoginPannel.gameObject.SetActive(true);
        else if (pannelName == SlectionPannel.gameObject.name) SlectionPannel.gameObject.SetActive(true);
        else if (pannelName == CreateRoomPannel.gameObject.name) CreateRoomPannel.gameObject.SetActive(true);
        else if (pannelName == InsideRoomPannel.gameObject.name) InsideRoomPannel.gameObject.SetActive(true);
        else if (pannelName == ListRoomPannel.gameObject.name) ListRoomPannel.gameObject.SetActive(true);
        else if (pannelName == chatPannel.gameObject.name) chatPannel.gameObject.SetActive(true);

    }

    public void CreateRoom()
    {

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(RoomName.text, roomOptions);
    }


    public void ListRoomsClicked()
    {

        PhotonNetwork.JoinLobby();
    }

    public void DestroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }



    public override void OnJoinedLobby()
    {
        Debug.Log("Da Lobby Has Been Joined");
        ActivatePannel("ListRooms");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Rooms Update: " + roomList.Count);

        DestroyChildren(listRoomPanelContent);
        UpdateCachedRoomList(roomList);

        foreach (var room in cachedRoomList)
        {
            var newRoomEntry = Instantiate(roomEntryPrefab, listRoomPanelContent);
            var newRoomEntryScript = newRoomEntry.GetComponent<RoomEntry>();
            newRoomEntryScript.roomName = room.Key;
            newRoomEntryScript.roomText.text = string.Format("[{0} - ({1}/{2})]", room.Key, room.Value.PlayerCount, room.Value.MaxPlayers);
        }

    }
    public void LeaveLobbyClicked()
    {
        PhotonNetwork.LeaveLobby();

    }




    public override void OnLeftLobby()
    {
        Debug.Log("Left Lobby!");

        DestroyChildren(listRoomPanelContent);
        DestroyChildren(InsideRoomPlayerList);
        cachedRoomList.Clear();

        ActivatePannel("Selection");

    }


    public void UpdateCachedRoomList(List<RoomInfo> roomList)
    {

        foreach (var room in roomList)
        {
            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
                cachedRoomList.Remove(room.Name);

            else
                cachedRoomList[room.Name] = room;
        }

    }


    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {

        Debug.Log("A Player joined the Room");

        var PlayerListEntry = Instantiate(TextPrefab, InsideRoomPlayerList);
        PlayerListEntry.GetComponent<Text>().text = newPlayer.NickName;
        PlayerListEntry.name = newPlayer.NickName;

    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("A Player left the Room");


        foreach (Transform child in InsideRoomPlayerList)
        {

            if (child.name == otherPlayer.NickName)
            {

                Destroy(child.gameObject);
                break;
            }

        }

    }

    public void JoinRandomRoomClicked()
    {

        PhotonNetwork.JoinRandomRoom();
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Sorry bro, couldn't join the Room: " + message);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Sorry bro, couldn't join the Random Room: " + message);
    }
}