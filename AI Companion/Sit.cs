using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sit : MonoBehaviour
{

    Companion companion;


    private bool collider1Entered;
    private bool collider2Entered;


    // Start is called before the first frame update
    void Start()
    {



        //GameObject fox = GameObject.Find("root");

        companion = FindObjectOfType<Companion>();

        //companion = fox.GetComponent<Companion>();
    }

    // Update is called once per frame
    void Update()
    {




        if (collider1Entered && collider2Entered)
        {

            if (Input.GetKeyDown(KeyCode.S))
            {

                companion.Sit();

            }
        }






    }







    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Companion"))
        {
            collider1Entered = true;
        }
         if (other.CompareTag("Player"))
        {
            collider2Entered = true;
        }
    }





    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Companion"))
        {
            collider1Entered = false;
        }
        else if (other.CompareTag("Player"))
        {
            collider2Entered = false;
        }
    }







    //private void OnTriggerStay(Collider other)
    //{
    //    //if (other.tag == "Companion" && other.tag == "Player")
    //    //{

    //    //    if (Input.GetKeyDown(KeyCode.S))
    //    //    {

    //    //        companion.Sit();

    //    //    }



    //    //}


    //    if (collider1Entered && collider2Entered && other.tag == "Companion")
    //    {

    //        if (Input.GetKeyDown(KeyCode.S))
    //        {

    //            companion.Sit();

    //        }
    //    }
    //}
}
