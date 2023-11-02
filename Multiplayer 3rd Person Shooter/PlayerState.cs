using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayerState : MonoBehaviour, IPunObservable
{
    public float Health = 300;
    public float TotalHealth;

    public Slider HealthBar;

    GameObject ManagerFinder;
    MultiplayerLevelManager Manager;




    private void Start()
    {
        TotalHealth = Health;
        HealthBar.maxValue = Health;
        HealthBar.value = Health;

        ManagerFinder = GameObject.Find("MultiplayerLevelManager");
        Manager = ManagerFinder.gameObject.GetComponent<MultiplayerLevelManager>();

      

    }


   public void OnPhotonSerializeView(PhotonStream stream , PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
           
            stream.SendNext(Health);
            stream.SendNext(HealthBar.value);

        }

        else
        {
            Health = (float)stream.ReceiveNext();
            HealthBar.value = (float)stream.ReceiveNext();


        }

    }


    public void Damage(BulletMultiplayer bullet)
    {
        print("Shit, Ive Been Hit " + bullet.Power);
        print("My HP Was " + Health);
        Health -= bullet.Power;
        print("No It's " + Health);
        HealthBar.value = Health;


        if (Health <= 0)
        Death(bullet);
       





    }




    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.gameObject.CompareTag("Bullet"))
    //    {
    //        Debug.Log("Hit");
    //        BulletMultiplayer bullet = collision.gameObject.GetComponent<BulletMultiplayer>();


    //        Damage(bullet);

    //    }


    //    if (Health <= 0)
    //    {

    //        Destroy(gameObject);

    //    }



    //}


    public void Death(BulletMultiplayer bullet)
    {


        bullet.owner.AddScore(1);
      

        gameObject.transform.position= Vector3.zero;
        Health = TotalHealth;
        HealthBar.value = HealthBar.maxValue;
    }
}
