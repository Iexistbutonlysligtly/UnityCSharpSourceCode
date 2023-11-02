using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SocialPlatforms.Impl;

public class GlobalLeaderboard : MonoBehaviour
{

    int MaxResults = 5;

    public LeaderboardPopup LeaderboardPopup;
   public void SubmitScore(int PlayerScore)
    {


        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
        {

            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate()
                {
                     StatisticName = "Most Kills",
                     Value = PlayerScore,

                }


            }


        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, PlayFabUpdateStatsResults,PlayFabUpdateStatsError);

    }


    void PlayFabUpdateStatsResults(UpdatePlayerStatisticsResult UpdatePlayerStatisticResults)
    {

        Debug.Log("PlayFab - Score Submitted.");

    }

    void PlayFabUpdateStatsError(PlayFabError UpdatePlayerStatisticsError)
    {

        Debug.Log("PlayFab - Error occurred ehilr submitting score: " + UpdatePlayerStatisticsError.ErrorMessage);

    }


   public void GetLederboard()
    {


        GetLeaderboardRequest request = new GetLeaderboardRequest()
        {

            MaxResultsCount = MaxResults,
            StatisticName = "Most Kills"



        };

        PlayFabClientAPI.GetLeaderboard(request,PlayFabGetLeaderboardResult, PlayFabGetLeaderboardError);

    }


    void PlayFabGetLeaderboardResult(GetLeaderboardResult GetLeaderboardResult)
    {

        Debug.Log("PlayFab - Get Leaderboard Completed. ");

        LeaderboardPopup.UpdateUI(GetLeaderboardResult.Leaderboard);
    }


    void PlayFabGetLeaderboardError(PlayFabError GetLeaderboardError)
    {

        Debug.Log("PlayFab - Error occured while getting Leaderboard: " + GetLeaderboardError.ErrorMessage);

    }
}
