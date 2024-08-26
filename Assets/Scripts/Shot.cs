using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public BulletCounter bulletCounter; // Referencia al script BulletCounter


    public float shotForce = 1000;
    public float shotRate = 0.1f;

    private float shotRateTime = 0;
    public AudioSource shootAudioSource; // Referencia al AudioSource para el sonido de disparo
    public AudioClip shootClip; // Clip de sonido para el disparo

    void Start()
    {
        if (shootAudioSource != null && shootClip != null)
        {
            shootAudioSource.clip = shootClip; // Asignar el clip de disparo al AudioSource
        }
    }

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            if (Time.time > shotRateTime)
            {
                GameObject newBullet;
                newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

                // Asigna el BulletCounter al script de la bala
                Bullet bulletScript = newBullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.bulletCounter = bulletCounter;
                }

                // Reproducir el sonido de disparo
                if (shootAudioSource != null)
                {
                    shootAudioSource.PlayOneShot(shootClip);
                }

                shotRateTime = Time.time + shotRate;
                bulletCounter.AddBullet(); // Añadir una bala al contador

                Destroy(newBullet, 2);
                StartCoroutine(RemoveBulletAfterTime(2f)); // Remover la bala del contador después de que se destruya
            }
        } 
    }

    IEnumerator RemoveBulletAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        bulletCounter.RemoveBullet();
    }
}
