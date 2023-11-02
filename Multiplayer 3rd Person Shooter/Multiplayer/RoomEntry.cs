using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomEntry : MonoBehaviour
{
 public TMP_Text roomText;
 public string roomName;


public void JoinRoom()
{
PhotonNetwork.LeaveLobby();    
PhotonNetwork.JoinRoom(roomName);
}



}
