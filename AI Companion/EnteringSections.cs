using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnteringSections : MonoBehaviour
{


    TMP_Text infoText;
    GUISystem GUI;

    TestRoomControls shoot;

    GameObject rainSwitch;
    Rain rain;

    // Start is called before the first frame update



    private void Awake()
    {
        GameObject iT = GameObject.Find("InfoText");
        infoText = iT.GetComponent<TMP_Text>();


        GUI = FindObjectOfType<GUISystem>();

        shoot = FindObjectOfType<TestRoomControls>();

        rainSwitch = GameObject.Find("RainSwitch");
        rain = rainSwitch.GetComponent<Rain>();

    }


    // Update is called once per frame



    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Player")
        {

            GUI.IButton();



        }



    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {


            if (gameObject.name == "FetchPart")
            {
                infoText.text = "Press Space to shoot a ball. Click on each and see if hell fetch it";


                if (Input.GetKeyDown(KeyCode.F))
                {

                    shoot.ShootBall();

                }

            }


            else if (gameObject.name == "SitPart")
            {
                infoText.text = "Bring him to the mat and press S ro see if he sits";

            }


            else if (gameObject.name == "ShelterPart")
            {
                infoText.text = "Press R to open the roof to allow it to rain. Get him to associate the hut with shelter";

            }


            else if (gameObject.name == "Switch")
            rain.enabled = true;







            

         



        }
    }

    private void OnTriggerExit(Collider other)
    {
        infoText.text = "Go up to one of the items to play-test the companion";

          
            rain.enabled = false;
    }
}
