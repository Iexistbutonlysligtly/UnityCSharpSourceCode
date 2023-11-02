using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    Rigidbody rigidBody;

    public float bulletSpeed = 15f;

    public AudioClip BulletHitAudio;
    public GameObject bulletImpactEffect;

    public float bulletLifeTime = 10f;

    public float Power = 10;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

       
    }

    public void InitializeBullet()
    {

        rigidBody.velocity *= bulletSpeed;
        Destroy(gameObject, bulletLifeTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.Play3D(BulletHitAudio, transform.position);

        VFXManager.Instance.PlayVFX(bulletImpactEffect, transform.position);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
          Enemy H = collision.gameObject.GetComponent<Enemy>();


          float TotalHealth = H.TotalHealth;
          H.Health -= Power;
            H.HealthBar.value = H.Health / TotalHealth;

        }

        Destroy(gameObject);
    }




}
