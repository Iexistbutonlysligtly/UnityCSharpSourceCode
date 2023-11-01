using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chips : MonoBehaviour
{
    private Animator eat;
    public bool foodVisible;
    public GameObject chips;
    public GameObject paket;

    void Start()
    {
        eat = GetComponent<Animator>();
       
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            eat.Play("Eating");
        }

        else
        {
            eat.Play("Not Eating");
        }


        if (foodVisible)
        {
            chips.GetComponent<MeshRenderer>().enabled = true;
            paket.GetComponent<MeshRenderer>().enabled = true;
        }

        else if (!foodVisible)
        {
            chips.GetComponent<MeshRenderer>().enabled = false;
            paket.GetComponent<MeshRenderer>().enabled = false;
        }

        if ((!foodVisible) && (Input.GetKeyDown(KeyCode.C)))
        {
            foodVisible = true;
        }

        else if ((foodVisible) && (Input.GetKeyDown(KeyCode.C)))
        {
            foodVisible = false;
        }

        
        

        
    }
    
    

}
