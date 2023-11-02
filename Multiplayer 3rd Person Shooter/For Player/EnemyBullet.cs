using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody rigidBody;

    public float bulletSpeed = 15f;

    public AudioClip BulletHitAudio;
    public GameObject bulletImpactEffect;

    public float bulletLifeTime = 10f;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();


    }

    public void InitializeEnemyBullet(Vector3 originalDirection)
    {
        transform.forward = originalDirection;
        rigidBody.velocity = transform.forward * bulletSpeed;
        Destroy(gameObject, bulletLifeTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.Play3D(BulletHitAudio, transform.position);

        VFXManager.Instance.PlayVFX(bulletImpactEffect, transform.position);

        Destroy(gameObject);
    }
}
