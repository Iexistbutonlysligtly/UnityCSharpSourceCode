using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData
{
    public string uid;
    public string username;
    public string score;
    public int bestScore;
    public string bestScoreDate;
    public int totalPlayersInRoom;
    public string roomName;


    public PlayerData()
    {
        //Generates a New Global Uniqe Identifer and Makes It a String to Put in The uid Stiring
        uid = Guid.NewGuid().ToString();


    }

}
