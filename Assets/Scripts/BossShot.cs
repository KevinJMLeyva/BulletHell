using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootingScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public BulletCounter bulletCounter; // Referencia al script BulletCounter
    public AudioSource shootAudioSource; // Referencia al AudioSource para el sonido de disparo
    public AudioClip shootClip; // Clip de sonido para el disparo

    public float shotForce = 1000f;
    public float shotRate = 0.1f;
    public float healthThreshold90 = 0.9f; // Porcentaje de vida al que el jefe comienza a disparar en 4 direcciones
    public float healthThreshold50 = 0.5f; // Porcentaje de vida al que el jefe comienza a disparar en 8 direcciones
    public float healthThreshold40 = 0.4f; // Porcentaje de vida al que el jefe comienza a disparar en media luna
    public float healthThreshold10 = 0.03f; // Porcentaje de vida al que el jefe comienza a disparar en círculo

    private float shotRateTime = 0f;
    private int directionCount = 1; // Número de direcciones en las que el jefe dispara

    public Enemy bossHealth; // Referencia al script de la salud del jefe

    void Start()
    {
        if (shootAudioSource != null && shootClip != null)
        {
            shootAudioSource.clip = shootClip; // Asignar el clip de disparo al AudioSource
        }
    }

    void Update()
    {
        if (bossHealth != null)
        {
            // Cambiar la dirección de disparo basado en el porcentaje de salud
            if (bossHealth.health <= bossHealth.maxHealth * healthThreshold10)
            {
                directionCount = 360; // Disparar en forma de círculo
            }
            else if (bossHealth.health <= bossHealth.maxHealth * healthThreshold40)
            {
                directionCount = 361; // Disparar en forma de media luna
            }
            else if (bossHealth.health <= bossHealth.maxHealth * healthThreshold50)
            {
                directionCount = 8; // Disparar en 8 direcciones
            }
            else if (bossHealth.health <= bossHealth.maxHealth * healthThreshold90)
            {
                directionCount = 4; // Disparar en 4 direcciones
            }
            else
            {
                directionCount = 1; // Disparar en una sola dirección
            }
        }

        if (Time.time > shotRateTime)
        {
            Shoot();
            shotRateTime = Time.time + shotRate;
        }
    }

    void Shoot()
    {
        if (directionCount == 360)
        {
            // Disparar en forma de círculo
            int numBullets = 36; // Número de balas en el círculo
            float angleStep = 360f / numBullets; // Ángulo entre cada bala
            for (int i = 0; i < numBullets; i++)
            {
                float angle = i * angleStep; // Calcular el ángulo para cada bala
                Quaternion rotation = Quaternion.Euler(0, angle, 0); // Crear una rotación para cada dirección
                FireBullet(rotation);
            }
        }
        else if (directionCount == 361)
        {
            // Disparar en forma de media luna
            int numBullets = 18; // Número de balas en la media luna
            float angleStep = 180f / numBullets; // Ángulo entre cada bala en el arco de 180 grados
            float startAngle = -90f; // Ángulo de inicio para el arco de media luna
            for (int i = 0; i < numBullets; i++)
            {
                float angle = startAngle + i * angleStep; // Calcular el ángulo para cada bala
                Quaternion rotation = Quaternion.Euler(0, angle, 0); // Crear una rotación para cada dirección
                FireBullet(rotation);
            }
        }
        else if (directionCount == 8)
        {
            // Disparar en 8 direcciones
            float angleStep = 360f / 8; // Dividir el círculo en 8 direcciones
            for (int i = 0; i < 8; i++)
            {
                float angle = i * angleStep;
                Quaternion rotation = Quaternion.Euler(0, angle, 0); // Crear una rotación para cada dirección
                FireBullet(rotation);
            }
        }
        else if (directionCount == 4)
        {
            // Disparar en 4 direcciones
            float angleStep = 360f / 4; // Dividir el círculo en 4 direcciones
            for (int i = 0; i < 4; i++)
            {
                float angle = i * angleStep;
                Quaternion rotation = Quaternion.Euler(0, angle, 0); // Crear una rotación para cada dirección
                FireBullet(rotation);
            }
        }
        else
        {
            // Disparar en una sola dirección
            FireBullet(spawnPoint.rotation);
        }
    }

    void FireBullet(Quaternion rotation)
    {
        // Instanciar la bala
        GameObject newBullet = Instantiate(bullet, spawnPoint.position, rotation);
        
        // Obtener el componente Rigidbody de la bala
        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            // Restablecer la velocidad
            bulletRigidbody.velocity = Vector3.zero; 
            bulletRigidbody.angularVelocity = Vector3.zero;
            
            // Aplicar la fuerza en la dirección correcta
            Vector3 direction = rotation * Vector3.forward; // La rotación se aplica para obtener la dirección
            bulletRigidbody.AddForce(direction * shotForce, ForceMode.Impulse);
        }

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

        bulletCounter.AddBullet(); // Añadir una bala al contador
    }
}
