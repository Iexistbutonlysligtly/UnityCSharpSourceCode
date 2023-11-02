using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    Transform FindChain;
    Rigidbody ChainWW;
    public GameObject Chain;
    public GameObject Ship;



    public Flap shipProps;
    public Hook Hook;


    public float fuelHolder;


    //public bool DoneCheck = false;



    public int ChildCount;

  public  float chainWeight;
    public float fullWeight;
    // Start is called before the first frame update
    void Start()
    {
       

        fuelHolder = shipProps.fuelCost;

        TotalWeight();


    }

    // Update is called once per frame
    void Update()
    {

        CheckWeight();
       



    }


    void TotalWeight()
    {
        fullWeight = 0;
        chainWeight = 0;

           var ShipWeight = Ship.GetComponent<Rigidbody>();

        for (int i = 0; i < ChildCount; i++)
        {

            FindChain = Chain.transform.GetChild(i);

         var ChainW = FindChain.gameObject.GetComponent<Rigidbody>();

            chainWeight += ChainW.mass;

            //ChainWW = ChainW.mass;



           
        }





      
        fullWeight = ShipWeight.mass + chainWeight + Hook.weight;
        shipProps.fuelCost = fuelHolder * fullWeight;



    }


    void CheckWeight()
    {

     if (Hook.CheckTrigger == true)
        {
            TotalWeight();

            print("69");

            Hook.CheckTrigger = false;



        }

        if (Hook.CheckTrigger2 == true)
        {
            TotalWeight();

            print("10");

            Hook.CheckTrigger2 = false;



        }





    }









}
