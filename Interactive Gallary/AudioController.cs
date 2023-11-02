using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioController : MonoBehaviour
{
    

    [Header("Audio Stuff")]
    public string audioName;

    public string t;/*= "C:\\Users\\User\\Desktop\\Test Please\\";
*/



    public AudioSource audioSource;
    public AudioClip audioClip;
    public string soundPath;


    public TMP_InputField InportText,InportSongName;

    [Header("Error Messege")]

    public TMP_Text ErrorText;







    //private void Awake()
    //{
    //    audioSource = gameObject.AddComponent<AudioSource>();
    //    //soundPath = "file://" + Application.streamingAssetsPath + "/Sound 33/";


    //    soundPath = "file://" + t;

    //    StartCoroutine(LoadAudio());
    //}



    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }



    void Start()
    {

        InportText.onEndEdit.AddListener(SubmitName);

        InportSongName.onEndEdit.AddListener(SubmitSongName);


  


    }


     void Update()
    {
        if (!(InportText.text == "") && !(InportSongName.text == "") && !audioSource.isPlaying && !(InportText.isFocused) &&!(InportSongName.isFocused))
        {
            ErrorText.SetText("Dicerctory and/or Song Name Incorrect.");

        }

        else if (audioSource.isPlaying && !(audioClip.name == audioName))
        {
            ErrorText.SetText("Dicerctory and/or Song Name Incorrect.");

        }

        else
        {
            ErrorText.SetText("");

        }

    }



    private void SubmitName(string arg0)
    {

        Debug.Log(arg0);

        t = arg0;

        soundPath = "file://" + t + '\\';

        //StartCoroutine(LoadAudio());


        //t = InportText.GetComponent<Text>().text;

        //soundPath = "file://" + t;


        //StartCoroutine(LoadAudio());

    }



    private void SubmitSongName(string arg0)
    {

        audioName = arg0;

        StartCoroutine(LoadAudio());


        //t = InportText.GetComponent<Text>().text;

        //soundPath = "file://" + t;


        //StartCoroutine(LoadAudio());

    }





    private IEnumerator LoadAudio()
    {
        WWW request = GetAudioFromFile(soundPath, audioName);
        yield return request;

        audioClip = request.GetAudioClip();
        audioClip.name = audioName;

        PlayAudioFile();
    }

    private void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;
    }

    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }
}
