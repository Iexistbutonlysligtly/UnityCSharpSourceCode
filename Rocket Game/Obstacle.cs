using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Obstacle : MonoBehaviour
{
    public float pushBackForce;
    public float powerDamage;
    public Flap Player;
    public string playerTag;
    public string objectTag;

    public AudioClip HitSound;
    AudioSource SoundSource;

    void Start()
    {

        SoundSource = gameObject.AddComponent<AudioSource>();
     
        SoundSource.clip = HitSound;
        
        SoundSource.enabled = false;
       


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == playerTag || collision.gameObject.tag == objectTag)
        {
            SoundSource.enabled = true;

            print("Hit");

           

          

            var PlayerRigid = Player.GetComponent<Rigidbody>();
           

            PlayerRigid.AddForce(-transform.right * pushBackForce);
            PlayerRigid.velocity *= -1;

            Player.exhaustFuel -= powerDamage;

            CameraShaker.Instance.ShakeOnce(4f,4f,.1f,.1f);


            StartCoroutine(SourceReset());


        }
    }



    IEnumerator SourceReset()
    {

       

        yield return new WaitForSeconds(1f);
        SoundSource.enabled = false;


    }
}


