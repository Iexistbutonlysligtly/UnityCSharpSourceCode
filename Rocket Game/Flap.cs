using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flap : MonoBehaviour
{
    public float flapForce = 80;
    public float fuelCost = 10;

    public float health = 100;
    public float exhaustFuel = 34000;

    public string DirectionTrack = "";


    public float totalWeight;


    public float minSpeed;
    public float maxSpeed;
    float speed;

    public Input move;

    public AudioClip MovmentSound;
    AudioSource SoundSource;






    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        SoundSource = gameObject.AddComponent<AudioSource>();
        SoundSource.loop = true;
        SoundSource.clip = MovmentSound;
       

    }






    // Update is called once per frame
    public void FixedUpdate()
    {

        if (!Input.anyKey)
        {
            DirectionTrack = "";

            SoundSource.enabled = false;

        }




        exhaustFuel = Mathf.Clamp(exhaustFuel, 0, exhaustFuel); ;

        Controls();
        Weight();




        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }


    public void Controls()
    {

        if (Input.GetKey(KeyCode.Space) || (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow))))
        {

            DirectionTrack = "Up";
            SoundSource.enabled = true;

            GetComponent<Rigidbody>().AddForce(transform.up * flapForce);

            StartCoroutine("Fule");

        }


        if (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow)))
        {

            DirectionTrack = "Down";
            SoundSource.enabled = true;

            GetComponent<Rigidbody>().AddForce(-transform.up * flapForce);

            StartCoroutine("Fule");
        }

        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            DirectionTrack = "Left";
            SoundSource.enabled = true;

            GetComponent<Rigidbody>().AddForce(-transform.right * flapForce);

            StartCoroutine("Fule");
        }

        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
        {
            DirectionTrack = "Right";
            SoundSource.enabled = true;

            GetComponent<Rigidbody>().AddForce(transform.right * flapForce);

            StartCoroutine("Fule");
        }

    }


    IEnumerator Fule()
    {
      

        exhaustFuel -= fuelCost;


        yield return new WaitForSeconds(1f);
    }



    void Weight()
    {
        var Weight = gameObject.GetComponent<Rigidbody>();

        totalWeight = Weight.mass;

    }

}