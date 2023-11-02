using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{

    public int score;
    public int winningScore;

    public Animator WinScreen;
    public string animatinTrigger;

    public Animator LoseScreen;
    public string loseAnimatinTrigger;

    public Animator PauseScreen;
    public string PauseAnimationTrigger;
    public string UnPauseAnimationTrigger;


    public Animator PreS;


    public TMP_Text scoreText;
    public Rigidbody CharacterToPuase;

    private Flap Player;
    public DropZone Zone;

    public bool isPaused;
    public bool showControls;

    public Camera Main;
    public Camera FarView;

    public CanvasGroup PreScreen;

    private void Awake()
    {
        Physics.gravity = new Vector3(0, -2.0f, 0);

     Player = (Flap)FindObjectOfType(typeof(Flap));


        Main.enabled = true;
        FarView.enabled = false;



    }

    private void Update()
    {
        score = Zone.objectCount;
        Pause();
        Controls();
        SwitchCam();

        if (score >= winningScore)
        {

            StartCoroutine(Winning());
        }


        if (Player.exhaustFuel <= 0)
        {

            StartCoroutine(Failed());
        }


        scoreText.text = score.ToString();
      

    }


    IEnumerator Winning()
    {

        //print("Won");


        WinScreen.SetTrigger(animatinTrigger);
        CharacterToPuase.isKinematic = true;

        yield return new WaitForSeconds(2f);
     
        
      

        

        
    }


    IEnumerator Failed()
    {

        //print("Won");


        LoseScreen.SetTrigger(loseAnimatinTrigger);
        CharacterToPuase.isKinematic = true;

        yield return new WaitForSeconds(2f);







    }

    void Pause()
    {



        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {

            isPaused = true;

            if (isPaused)
            {
                print("p");

                PauseScreen.SetTrigger(PauseAnimationTrigger);
                CharacterToPuase.isKinematic = true;

                gameObject.GetComponent<AudioSource>().Pause();
            }

        }


        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {

            isPaused = false;

            
            
            


                PauseScreen.SetTrigger(UnPauseAnimationTrigger);
            CharacterToPuase.isKinematic = false;


            gameObject.GetComponent<AudioSource>().Play();




        }


       







    }

    void Controls()
    {

        if (Input.GetKeyDown(KeyCode.C) && !showControls)
        {
            PreS.enabled = false;

            showControls = true;

            PreScreen.alpha = 1;
           
            CharacterToPuase.isKinematic = true;

            gameObject.GetComponent<AudioSource>().Pause();
        }


        else if (Input.GetKeyDown(KeyCode.C) && showControls)
        {
            showControls = false;

            PreScreen.alpha = 0;

            CharacterToPuase.isKinematic = false;

            gameObject.GetComponent<AudioSource>().Play();
        }




    }



    void SwitchCam()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Main.enabled = !Main.enabled;
            FarView.enabled = !FarView.enabled;

        }
    }
}
