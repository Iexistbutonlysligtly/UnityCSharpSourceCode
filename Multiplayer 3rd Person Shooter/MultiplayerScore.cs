using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class MultiplayerScore : MonoBehaviourPunCallbacks
{

    public GameObject PlayerScorePreab;
    public Transform pannel;

    Dictionary<int, GameObject> PlayerScore = new Dictionary<int, GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        foreach (var player in PhotonNetwork.PlayerList)
        {

            player.SetScore(0);
            var playerScoreObject = Instantiate(PlayerScorePreab, pannel);
            var platerScoeObjectText = playerScoreObject.GetComponent<Text>();
            platerScoeObjectText.text = string.Format("{0} Score: {1}", player.NickName, player.GetScore());


            PlayerScore[player.ActorNumber] = playerScoreObject;
        }
        
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        var playerScoreObject = PlayerScore[targetPlayer.ActorNumber];
        var playerScoreObjectText = playerScoreObject.GetComponent<Text>();
        playerScoreObjectText.text = string.Format("{0} Score: {1}", targetPlayer.NickName, targetPlayer.GetScore());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
