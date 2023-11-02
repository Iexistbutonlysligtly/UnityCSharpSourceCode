using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalBestPopUp : MonoBehaviour
{

    public GameObject scoreHolder;
    public GameObject noScoreText;

    public Text userName;
    public Text bestScore;
    public Text Date;
    public Text TotalPlayers;
    public Text RoomName;

    public void UpdatePersonalBestUI()
    {

        PlayerData playerData = GameManager.Instance.playerData;

        if (playerData.username != null)
        {


            userName.text = playerData.username;
            bestScore.text = playerData.bestScore.ToString();
            Date.text = playerData.bestScoreDate;
            TotalPlayers.text = playerData.totalPlayersInRoom.ToString();
            RoomName.text = playerData.roomName;

            scoreHolder.SetActive(true);
            noScoreText.SetActive(false);


        }

        else
        {
            scoreHolder.SetActive(false);
            noScoreText.SetActive(true);

        }

    }

    private void OnEnable()
    {
        UpdatePersonalBestUI();
    }



}
