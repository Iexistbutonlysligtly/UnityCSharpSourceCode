using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Timer : MonoBehaviour
{

    public float TimeLeft;
    public bool TimerOn = false;

    public TMP_Text TimeText;


    // Start is called before the first frame update
    void Start()
    {
        TimerOn= true;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {

                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);

            }


        }


    }


    void updateTimer(float currentTime)
    {

        currentTime += 1;

        float minuntes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);


        TimeText.text = string.Format("{0:00} : {1:00}", minuntes, seconds);

    }



   //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
   // {

   //     if (stream.IsWriting)
   //     {

   //         stream.SendNext(TimeLeft);
   //         stream.SendNext(this.minuntes);

   //     }

   //     else
   //     {
   //         TimeLeft = (float)stream.ReceiveNext();
   //         HealthBar.value = (float)stream.ReceiveNext();


   //     }

   // }
}
