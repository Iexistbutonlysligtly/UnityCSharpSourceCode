using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofCheck : MonoBehaviour
{

   public bool Sheltered;


    

    Companion Companion;

    Rain rain;
  




    // Start is called before the first frame update
    void Start()
    {

        Companion = FindObjectOfType<Companion>();
        rain = FindObjectOfType<Rain>();
       
    }

    // Update is called once per frame
    void Update()
    {
        RoofC();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shelter") {

            Sheltered = true;

        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        Sheltered = false;
    }



    private void RoofC()
    {
        if (!Sheltered && rain.raining)
        {

            Companion.currentState = "Distressed";

        }

        else if (Sheltered && rain.raining)
            Companion.currentState = "Normal";
    }
}
