using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Realtime;

public class ShootingForMultiplayer : MonoBehaviour
{

    //Elemental Bullet
    public GameObject ChikiaraBlast;
    public Transform SL;

    //public Rigidbody BRB;


    //Elemental Bullet Force
    //public GameObject CBlast_UpForce, CBlast_ShootForce;


    //Elemental Bullet Stats
    public bool Holdabale; // If the character can shoot consistently if button is down
    public int ChikiaraEnergyAmmount, EnergyBlastsPerShot; // The total energy of the character and amoout thry use per blast
    public float RegeneratingEnergy, EnergyRegenTime, TimeBetweenShots, Spread;

    public float speed;

    public int ChikiaraEnergyLeft, ChikiaraBlastsDone;

    //States
   public bool IsShooting, ReadyToShoot, IsRegeneratingEnergy;



    Vector3 mouseWorldPosition;
  //  Vector3 spawnBulletPosition = GameObject.Find("B").transform.position;


    RaycastHit hit;

    public bool Range, Invoking;

    public Camera MC;

    
    
    

    public AudioClip BlastSound;
    public GameObject bulletFiringEffect;


    [SerializeField] PhotonView photonView;


    void Start()
    {
        ChikiaraEnergyLeft = ChikiaraEnergyAmmount;
        ReadyToShoot = true;
        hit = new RaycastHit();

        mouseWorldPosition = Input.mousePosition;

        //photonView = GetComponent<PhotonView>();

    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    ShootingInput();
    //}

    //void ShootingInput()
    //{
    //    if (Holdabale) IsShooting = Input.GetKey(KeyCode.Mouse0);
    //    else IsShooting = Input.GetKeyDown(KeyCode.Mouse0);

    //    if ( ReadyToShoot && IsShooting && !Reloading && ChikiaraEnergyLeft > 0)
    //    {


    //    }

    //    ChikiaraBlastsDone = 0;

    //    Blast();

    //}

    //void Blast()
    //{
    //    ChikiaraEnergyLeft--;
    //    ChikiaraBlastsDone++;
    //}

  void Update()
    {
        if (!photonView.IsMine)
            return;   //Cheack if Values Belong To The Client

        // MyInput();
        photonView.RPC("MyInput", RpcTarget.AllViaServer);




    }
    [PunRPC]
    void MyInput()
    {

        if (Holdabale) IsShooting = Input.GetMouseButton(0);
        else IsShooting = Input.GetMouseButtonDown(0);


        if (Input.GetKeyDown(KeyCode.R) && ChikiaraEnergyLeft < ChikiaraEnergyAmmount && !IsRegeneratingEnergy) RegenEnergy();

        if (IsShooting && !IsRegeneratingEnergy && ReadyToShoot && ChikiaraEnergyLeft > 0)
        {
            ChikiaraBlastsDone = 0;

            if (!photonView.IsMine)
                return;   //Cheack if Values Belong To The Client


            //Shoot();

            photonView.RPC("Shoot", RpcTarget.AllViaServer);

            //photonView Handles The Clients Spesfic Network Functions
            //RPC Tells photonView That The Method Will Be an RCP Method
            //Then Say To Invoke Shoot
            // RpcTarget.AllViaServer); Uses the server to tell all the clinets to have their Shoot Method Invoked



        }








    }


    [PunRPC]
    void Shoot()
    {


        Debug.Log("Shot");
     

        if (!Range && ReadyToShoot)
        {
            Vector3 aimDir = (mouseWorldPosition - SL.transform.position).normalized;
            //Instantiate(ChikiaraBlast, SL.transform.position, Quaternion.LookRotation(aimDir, Vector3.up)); 
               
            GameObject cb = Instantiate(ChikiaraBlast, SL.position, Quaternion.LookRotation(aimDir, Vector3.up));
            cb.GetComponent<BulletMultiplayer>().InitializeBullet(photonView.Owner);

            //GameObject cb = Instantiate(ChikiaraBlast, SL.position, ChikiaraBlast.transform.rotation);
            Rigidbody Rig = cb.GetComponent<Rigidbody>();

            Rig.AddForce(SL.forward * speed, ForceMode.Impulse);

        }


       else if (Range && ReadyToShoot)
        {

            Ray ray = MC.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 400.0f))
            {
                GameObject newBall = Instantiate(ChikiaraBlast, SL.position, SL.rotation) as GameObject;
                newBall.GetComponent<BulletMultiplayer>().InitializeBullet(photonView.Owner);

                newBall.GetComponent<Rigidbody>().velocity = (hit.point - SL.position).normalized * speed;
            }
        }


        ReadyToShoot = false;


        ChikiaraEnergyLeft--;
        ChikiaraBlastsDone++;

        if (Invoking)
        {

            Invoke("ResetBlast", TimeBetweenShots);
            Invoking = false;

        }

        if (ChikiaraBlastsDone < EnergyBlastsPerShot && ChikiaraEnergyLeft > 0)
            Invoke("Shoot", TimeBetweenShots);
       
        AudioManager.Instance.Play3D(BlastSound, transform.position);
        VFXManager.Instance.PlayVFX(bulletFiringEffect, SL.position);

    }

    private void ResetBlast()
    {
        ReadyToShoot = true;
        Invoking= true;
    }

    private void RegenEnergy() {

        IsRegeneratingEnergy = true;
        Invoke("ComplatedEnergyRegen", EnergyRegenTime);
    
    }

    private void ComplatedEnergyRegen()
    {

        ChikiaraEnergyLeft = ChikiaraEnergyAmmount;
        IsRegeneratingEnergy = false;
    }

}
