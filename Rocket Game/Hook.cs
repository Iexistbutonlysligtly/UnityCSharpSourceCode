using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{

    public string ObjectTag;
    public GameObject TheHook;

    [HideInInspector]
    public bool A,B;

    public bool Holding;
    public float weight;
    Collision ObjectCollistion;
    float WeightHolder;

    [HideInInspector]
    public bool CheckTrigger = false;
    [HideInInspector]
    public bool CheckTrigger2 = false;

    public AudioClip AttatchSound;
    AudioSource SoundSource;



    // Start is called before the first frame update
    void Start()
    {
        SoundSource = gameObject.AddComponent<AudioSource>();

        SoundSource.clip = AttatchSound;

        SoundSource.enabled = false;

        SoundSource.volume = 1;


    }

    // Update is called once per frame
    void Update()
    {

        Attach();

    }


    public void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.tag == ObjectTag)
        {
            SoundSource.enabled = true;

            ///TheHook.AddComponent<HingeJoint>();
            TheHook.AddComponent<FixedJoint>();
            ObjectCollistion = collision;
            Holding = true;

            A = true;



            Invoke("Test", 0.02f);
            StartCoroutine(SourceReset());

        }

       

    }




    void Attach()
    {
        

        if (Holding)
        {
            if (!(ObjectCollistion.gameObject.tag == ObjectTag))
            {

                Deattach();
            }



            //var otherBody = TheHook.GetComponents<HingeJoint>();
            var otherBody = TheHook.GetComponents<FixedJoint>();
            var ObToCollide = ObjectCollistion.gameObject.GetComponent<Rigidbody>();

            var TagCheck = ObjectCollistion.gameObject;

            ObToCollide.isKinematic = true;
            otherBody[0].connectedBody = ObToCollide;
            ObToCollide.isKinematic = false;



            weight = ObToCollide.mass;

            

            // WeightHolder = ObToCollide.mass;


            if (Input.GetKey(KeyCode.L))
            {
                print("Let the Fuck Go");
                Deattach();
            }

           

            if (A)
            {
                B = true;
            }


            if( Holding && B)
            {

                Invoke("QuitAB", 0.02f);


            }



        }



       

        




    }

    public bool Test()
    {

        CheckTrigger = weight > 0;
      


        return CheckTrigger;

    }



    public bool Test2()
    {

        CheckTrigger2 = true;


        return CheckTrigger2;

    }

    void QuitAB()
    {


        A = false;
        B = false;



    }






   public void Deattach()
    {



        //var otherBody = TheHook.GetComponents<HingeJoint>();
        var otherBody = TheHook.GetComponents<FixedJoint>();
        var ObToCollide = ObjectCollistion.gameObject.GetComponent<Rigidbody>();


        //otherBody[1].connectedBody = null;

        Destroy(otherBody[0]);

        print("Let the Fuck Go");


        Holding = false;


        weight = 0;
        Invoke("Test2", 0.02f);



    }

    IEnumerator SourceReset()
    {



        yield return new WaitForSeconds(1f);
        SoundSource.enabled = false;


    }


    public void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == ObjectTag)
        {


            Invoke("Test", 0.02f);

        }



    }




    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == ObjectTag)
    //    {

    //        weight = 0;
    //        Invoke("Test2", 0.02f);

    //    }
    //}



}
