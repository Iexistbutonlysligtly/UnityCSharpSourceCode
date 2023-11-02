using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomControls : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public float shootForce = 10f;



    

    ThirdPersonControl player;
    AudioSource sound;

    // Update is called once per frame




    private void Start()
    {
        sound = GetComponent<AudioSource>();

     
    }

    void Update()
    {
      
    }

    public void ShootBall()
    {

        if (sound.isPlaying)
        {
           
           sound.Stop();

        }





        sound.clip = Resources.Load<AudioClip>("Cannon");
        sound.Play();


        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.AddForce(spawnPoint.forward * shootForce, ForceMode.Impulse);
    }
}
