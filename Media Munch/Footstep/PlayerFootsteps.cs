using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour

{
    //public AudioClip footSteps;
    //public AudioSource SoundSource;
    public bool isWalking;
    public bool isRunning;
    GameObject FPS;
    GameObject FPS2;

    void Start()
    {
        FPS = GameObject.Find("First Person Character");
        FPS2 = GameObject.Find("Main Camera");
        //SoundSource.clip = footSteps;

        
    }

    
    void Update()
    {

        if (isWalking)
        {
            FPS.GetComponent<AudioSource>().enabled = true;
        }

        if (!isWalking)
        {
            FPS.GetComponent<AudioSource>().enabled = false;
        }

        if (!Input.GetKey(KeyCode.W))
        {
            isWalking = false;
        }


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isWalking = true;
        }



        if (isRunning)
        {
            FPS2.GetComponent<AudioSource>().enabled = true;
            FPS.GetComponent<AudioSource>().enabled = false;
        }

        if (!isRunning)
        {
            FPS2.GetComponent<AudioSource>().enabled = false;
        }


        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)) 
        {
            isRunning = true;
        }

        else
        {
            isRunning = false;
        }

    }
}
