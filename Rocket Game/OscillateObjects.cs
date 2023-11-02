using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillateObjects : MonoBehaviour
{

    public float oscillateSpeed;
   [Range(0f,400f)]
    public float rotateSpeed;

    public float maxHight;
    public float maxLow;

    public bool GoingUp, GoingDown;










    //Variables for Testing

    float intialYPos;
    float ActivingPosH;
    float ActivingPosB;
    Vector3 H;
    Vector3 B;
    Rigidbody ThisObject;




    private void Start()
    {

        intialYPos = gameObject.transform.position.y;
        H = new Vector3(0, gameObject.transform.position.y +maxHight, 0);
        B = new Vector3(0, gameObject.transform.position.y - maxLow, 0);


        ThisObject = gameObject.GetComponent<Rigidbody>();

        ActivingPosH = H.y;
        ActivingPosB = B.y;
    }


    void FixedUpdate()
    {
        ThisObject.AddTorque(transform.up * rotateSpeed);
        Up();
        Down();
    }


    void Up()
    {

       

        if (transform.position.y < H.y && GoingUp)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * oscillateSpeed);
        }

        else if(transform.position.y >= H.y)
        {
            //ThisObject.velocity = -ThisObject.velocity;
            //ThisObject.angularVelocity = -ThisObject.angularVelocity;

          

            // print("It Passed The High");


            GoingUp = false;
            GoingDown = true;

            Down();

        }

        

       

    }


    void Down()
    {
        
        if (transform.position.y > B.y && GoingDown)
        {



            GetComponent<Rigidbody>().AddForce(-transform.up * oscillateSpeed);
        }







        else if (transform.position.y <= B.y)
        {
            //ThisObject.velocity = Vector3.zero;
            //ThisObject.angularVelocity = Vector3.zero;

          
            GoingDown = false;
            GoingUp = true;


          //  print("It Passed The Low");


            
        }



    }
}
