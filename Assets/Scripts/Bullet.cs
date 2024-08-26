using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f; // Daño que causa la bala
    public bool isEnemyBullet = false; // Si la bala fue disparada por un enemigo
    public BulletCounter bulletCounter; // Referencia al BulletCounter

    private bool hasCollided = false; // Para asegurarse de que el contador se actualice solo una vez

    void Start()
    {
        // Configura las capas para ignorar colisiones entre balas de enemigos y balas del jugador
        if (isEnemyBullet)
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("PlayerBullet"));
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Enemy"));
        }
        else
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"));
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"));
        }

        // Programar la eliminación después de un tiempo
        Destroy(gameObject, 2f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasCollided)
        {
            return; // Si la bala ya ha colisionado, no haga nada más
        }

        hasCollided = true;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null && isEnemyBullet)
            {
                playerHealth.TakeDamage(damage);
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null && !isEnemyBullet)
            {
                enemy.TakeDamage(damage);
            }
        }

        DestroyBullet();
    }

    void DestroyBullet()
    {
        if (bulletCounter != null)
        {
            bulletCounter.RemoveBullet(); // Asegúrate de que RemoveBullet solo se llama una vez
        }
        Destroy(gameObject);
    }
}
