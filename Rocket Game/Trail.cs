using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{

    public Flap MovmentTrigger;
   

    public enum Test { Up, Down, Left, Right}

    public Test TestingD;

    public string OptionHolder;



    // Update is called once per frame
    void FixedUpdate()
    {
        OptionHolder = TestingD.ToString();
        


        if (OptionHolder ==Test.Up.ToString())
        {
           Up();


        }


        if (OptionHolder == Test.Down.ToString())
        {
            Down(3f);
        }

        if (OptionHolder == Test.Left.ToString())
        {
            Left();
        }

         if (OptionHolder == Test.Right.ToString())
        {
            Right();
        }

    }









    void Right()
    {
        if (MovmentTrigger.DirectionTrack == "Right")
        {
            gameObject.GetComponent<TrailRenderer>().enabled = true;

        }

        else
        {
          
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        }



       
    }


   void Left()
    {
        if (MovmentTrigger.DirectionTrack == "Left")
        {
            gameObject.GetComponent<TrailRenderer>().enabled = true;

        }

        else
        {
           
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        }

    }

    void Up()
    {
        if (MovmentTrigger.DirectionTrack == "Up")
        {

            gameObject.GetComponent<TrailRenderer>().enabled = true;

        }

        else
        {
          
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        }



    }

   void Down(float time)
    {
        if (MovmentTrigger.DirectionTrack == "Down")
        {
            gameObject.GetComponent<TrailRenderer>().enabled = true;

        }

        else
        {
           
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        }




    }



    //IEnumerator Delay()
    //{


       


    //    yield return new WaitForSeconds(f);
    //}


}
