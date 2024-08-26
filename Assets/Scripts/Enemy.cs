using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f; // Vida inicial del enemigo

    public float maxHealth = 100f;
    public AudioSource footstepAudioSource; // Referencia al AudioSource para los sonidos de pasos

    void Start()
    {
        EnemyCounter.IncrementEnemyCount();
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        health -= damage;

        // Verificar si la salud es menor o igual a 0
        if (health <= 0f)
        {
            Die();
        }
    }

    // Método que se llama cuando el enemigo muere
    void Die()
    {
        if (footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop();
        }
        Destroy(gameObject); // Destruye al enemigo
        EnemyCounter.DecrementEnemyCount();
    }
}
