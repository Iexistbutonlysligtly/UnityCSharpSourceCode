using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leguar.TotalJSON;
using System.IO;
using System.Text;

using PlayFab;
using PlayFab.ClientModels;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public PlayerData playerData;
    public string filePath;

    static string Encrypted;
    int key = 369;


    public GlobalLeaderboard GlobalLeaderboard;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }

        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        LoadPlayerData();
        LoginToPlayFab();

       
    }

    void LoginToPlayFab()
    {


        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
        {
            CreateAccount = true,
            CustomId = playerData.uid,


        };


        PlayFabClientAPI.LoginWithCustomID(request, PlayFabLoginResult, PlayerFabLoginError);

    }




    void PlayFabLoginResult(LoginResult LoginResult)
    {

        Debug.Log("PlayFab - Login Succeeded " + LoginResult.ToJson());

    }





    void PlayerFabLoginError(PlayFabError LoginError)
    {

        Debug.Log("Player - Login failed: " + LoginError.ErrorMessage);

    }

    //Will Pass To The Callback Method bt PlayFab in The Event of a Login Error
    public string SavePlayerData()
    {
        string serialisedDataString = JSON.Serialize(playerData).CreateString(); // Puts Data In Jason Format 



        //Encodes The JASON Data
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(serialisedDataString);
        string returnValue = System.Convert.ToBase64String(plainTextBytes);


        //Calling The Encryption Method
        Encrypt(returnValue, key);





        //Puts The Encoded and Encryted Data In A File
        File.WriteAllText(filePath, Encrypted);

        return Encrypted;


    }






    public static string Encrypt(string EncodedData, int key)
    {
        //Encrtpts The Data

        StringBuilder input = new StringBuilder(EncodedData);

        StringBuilder output = new StringBuilder(EncodedData.Length);

        char character;


        for (int i = 0; i < EncodedData.Length; i++)
        {


            character = input[i];
            character = (char)(character ^ key);
            output.Append(character);

        }

        Encrypted = output.ToString();

        return Encrypted;


    }





    public string LoadPlayerData()

    {
        if (!File.Exists(filePath))
        {

            playerData = new PlayerData();  // Makes New Data File if There's None Already
            SavePlayerData();
        }


        //Reads The Data In The File
        string fileContents = File.ReadAllText(filePath);

        //Decrypts The Data In File Using The Key
        string DecryptedFileContents = Encrypt(fileContents, key);






        //Decodes The Decrypted Data
        byte[] base64DecodedBytes = System.Convert.FromBase64String(DecryptedFileContents);
        string returnValue = Encoding.UTF8.GetString(base64DecodedBytes);


        //Puts The Decoded and Decrypte Data Back in JASON Format 
        playerData = JSON.ParseString(returnValue).Deserialize<PlayerData>();

        return returnValue;


    }

}
