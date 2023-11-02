using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rain : MonoBehaviour
{
    public GameObject rainPrefab;
    public bool sw = false;
    private GameObject rainInstance;

    Animator Roof;


    public bool raining;

    Companion Companion;
    RoofCheck RC;

    AudioSource sounds;




    private void Start()
    {
        Companion = FindObjectOfType<Companion>();
        RC = FindObjectOfType<RoofCheck>();
        rainInstance = null;


        GameObject roof = GameObject.Find("Roof");
        Roof = roof.GetComponent<Animator>();

        sounds = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            sw = !sw;
            if (sw)
            {


               sounds.PlayOneShot(Resources.Load<AudioClip>("Switch"), 1F);




                StartCoroutine(RoofSound());
                Roof.Play("Roof");





    










                    StartCoroutine(SpawnRain());

                




               
            }
            else
            {




                StartCoroutine(RoofSound());
                // Play the animation in reverse
                Roof.Play("Roof 2");


                DestroyRain();
                Companion.currentState = "Normal";
            }
        }

    
    }




    private IEnumerator RoofSound()
    {

        yield return new WaitForSeconds(0.5f);

        sounds.PlayOneShot(Resources.Load<AudioClip>("Roof2"), 1F);



    }









    private IEnumerator SpawnRain()
    {

        yield return new WaitForSeconds(0.1f); // Adjust the delay time as needed




        while (!Roof.GetCurrentAnimatorStateInfo(0).IsName("Roof"))
        {
            yield return null;
        }

        while (Roof.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }





        rainInstance = Instantiate(rainPrefab, Vector3.zero, Quaternion.identity);

        raining = true;

       
    }

    void DestroyRain()
    {
        if (rainInstance != null)
        {
            Destroy(rainInstance);
            rainInstance = null;

            raining = false;
        }
    }

    //private void RoofC()
    //{
    //    if (!RC.Sheltered && raining)
    //    {

    //        Companion.currentState = "Distressed";

    //    }

    //    else if (RC.Sheltered && raining)
    //        Companion.currentState = "Normal";
    //}
}
