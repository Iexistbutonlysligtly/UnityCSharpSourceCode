using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    ThirdPersonControl Player;
    public float volume;
    // Start is called before the first frame update
    void Start()
    {
     Player = FindObjectOfType<ThirdPersonControl>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {

            float randomValue = Random.value;
            //  Player.footStep.loop = false;

            if (randomValue % 2 == 0)
            {

                Player.footStep.PlayOneShot(Resources.Load<AudioClip>("FStep1"), volume);

            }

            else
            {
                Player.footStep.PlayOneShot(Resources.Load<AudioClip>("FStep2"), volume);


            }


            //  Debug.Log("fgb");

        }




     //   Debug.Log("fgb");


    }
}
