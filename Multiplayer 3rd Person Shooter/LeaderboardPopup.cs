using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LeaderboardPopup : MonoBehaviour
{

    public GameObject Score;
    public GameObject ScoreText;

    public GameObject LeaderboardItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateUI(List<PlayerLeaderboardEntry> playerLeaderboardEntries)
    {

        if (playerLeaderboardEntries.Count > 0)
        {

            DestroyChildren(Score.transform);
            for (int i = 0; i < playerLeaderboardEntries.Count; i++)
            {
                 GameObject newLeaderboardItem = Instantiate(LeaderboardItem, Vector3.zero, Quaternion.identity, Score.transform);

                newLeaderboardItem.GetComponent<LeaderboardItem>().SetScores(i + 1, playerLeaderboardEntries[i].DisplayName, playerLeaderboardEntries[i].StatValue);
            }



            Score.SetActive(true);
            ScoreText.SetActive(false);


        }

        else
        {

            Score.SetActive(false);
            ScoreText.SetActive(true);

        }

    }

    private void OnEnable()
    {
        GameManager.Instance.GlobalLeaderboard.GetLederboard();
    }



  void DestroyChildren(Transform parent)
    {

        foreach (Transform child in parent)
        {

            Destroy(child.gameObject);


        }

    }

}
