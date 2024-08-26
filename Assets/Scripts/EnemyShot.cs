using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public BulletCounter bulletCounter; // Referencia al script BulletCounter
    public AudioSource shootAudioSource; // Referencia al AudioSource para el sonido de disparo
    public AudioClip shootClip; // Clip de sonido para el disparo

    public float shotForce = 1000f;
    public float shotRate = 0.1f;

    private float shotRateTime = 0f;

    void Start()
    {
        if (shootAudioSource != null && shootClip != null)
        {
            shootAudioSource.clip = shootClip; // Asignar el clip de disparo al AudioSource
        }
    }

    void Update()
    {
        if (Time.time > shotRateTime)
        {
            // Instanciar la bala
            GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

            // Indicar que la bala es disparada por un enemigo
            Bullet bulletScript = newBullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.isEnemyBullet = true;
                bulletScript.bulletCounter = bulletCounter; // Asignar el BulletCounter al script de la bala
            }

            // Reproducir el sonido de disparo
            if (shootAudioSource != null)
            {
                shootAudioSource.PlayOneShot(shootClip);
            }

            shotRateTime = Time.time + shotRate;
            bulletCounter.AddBullet(); // Añadir una bala al contador

            // No necesitas el Coroutine RemoveBulletAfterTime aquí
        }
    }
}
