using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBar : MonoBehaviour
{
    //private float speed2 = 5f;
    public float speed = 2.5f;
    private float barStopper = 0.15f;
    private float barStart = 1.23f;
    private GameObject bar;
    Vector3 temp;
    GameObject eating;
    public GameObject chips;
    public GameObject paket;



    void Start()
    {
        temp.x = barStart;

        bar = GameObject.Find("Bar");

        eating = GameObject.Find("Bar");

        eating.GetComponent<AudioSource>().enabled = false;

    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && (chips.GetComponent<MeshRenderer>().enabled = true) && (paket.GetComponent<MeshRenderer>().enabled = true))
        {
            EatingFood();
        }

        else
        {
            BreakFromEating();
        }

        if (temp.x <= barStopper)
        {
            FinshedEating();
        }



        void EatingFood()
        {
            temp = transform.localScale;

            temp.x -= Time.deltaTime / speed;

            transform.localScale = temp;

            eating.GetComponent<AudioSource>().enabled = true;

            
           
        }

        void BreakFromEating()
        {
            eating.GetComponent<AudioSource>().enabled = false;
        }

        void FinshedEating()
        {
            eating.GetComponent<AudioSource>().enabled = false;
            bar.GetComponent<FoodBar>().enabled = false;
        }

        


            
        
    }
}
