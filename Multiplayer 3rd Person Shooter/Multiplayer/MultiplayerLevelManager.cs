using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;
using System;

public class MultiplayerLevelManager : MonoBehaviourPunCallbacks
{
    public int maxKill = 3;
    public GameObject gameOverPopup;
    public TMP_Text WinnerText;

    public Timer Timer;

    int HighestKills;

    int previous = 0;
    Player WinnigPlayer;

    private void Start()
    {
        PhotonNetwork.Instantiate("Multi-Placholder Player 1", Vector3.zero,Quaternion.identity);
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //CheckWinningPlayer();

        if (targetPlayer.GetScore() == maxKill)
        {
            WinnerText.text = targetPlayer.NickName;
            gameOverPopup.SetActive(true);

            PersonalBestScore();
        }
    }

    //private void Update()
    //{

    //    TimeUp();

    //}


    //public void TimeUp()
    //{
       

        
    //    if (Timer.TimeLeft <= 0)
    //    {

    //        WinnerText.text = WinnigPlayer.NickName;
    //        gameOverPopup.SetActive(true);

    //        PersonalBestScore();



    //    }

     
        
    //}


    //void CheckWinningPlayer()
    //{
      

    //    foreach (Player player in PhotonNetwork.PlayerListOthers)
    //    {




    //        if (player.GetScore() > previous)
    //        {
    //            HighestKills = player.GetScore();
    //            WinnigPlayer = player;
    //        }

    //        previous = player.GetScore();



    //    }


    //}

    void PersonalBestScore()
    {

        int currentScore = PhotonNetwork.LocalPlayer.GetScore();
        PlayerData playerData = GameManager.Instance.playerData;


        if (currentScore > playerData.bestScore)
        {
            playerData.username = PhotonNetwork.LocalPlayer.NickName;
            playerData.bestScore = currentScore;
            playerData.bestScoreDate = DateTime.UtcNow.ToString();
            playerData.totalPlayersInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
            playerData.roomName = PhotonNetwork.CurrentRoom.Name;

            GameManager.Instance.GlobalLeaderboard.SubmitScore(currentScore); 


            GameManager.Instance.SavePlayerData();


        }

    }


    public void LeaveGame()
    {

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Multiplayer");
    }

}
