using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Referencia al Transform del jugador
    public PlayerHealth playerHealth; // Referencia al script de la vida del jugador
    public float speed = 5f; // Velocidad a la que se mueve el enemigo
    public float stoppingDistance = 10f; // Distancia a la que el enemigo deja de acercarse
    public float minimumDistance = 1.5f; // Distancia mínima a la que el enemigo no se acerca más
    public AudioSource footstepAudioSource; // Referencia al AudioSource para los sonidos de pasos

    void Update()
    {
        // Si la vida del jugador es cero, detén el sonido de pasos y termina la ejecución de Update
        if (playerHealth.currentHealth <= 0)
        {
            // Detén el sonido de pasos si se está reproduciendo
            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop();
            }

            // No mover el enemigo si el jugador está muerto
            return;
        }

        // Calcula la distancia entre el enemigo y el jugador
        float distance = Vector3.Distance(transform.position, player.position);

        // Si la distancia es mayor que la distancia mínima y menor que la distancia de parada, el enemigo se mueve hacia el jugador
        if (distance > minimumDistance && distance > stoppingDistance)
        {
            // Calcula la dirección desde el enemigo hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;

            // Mueve al enemigo hacia el jugador
            transform.position += direction * speed * Time.deltaTime;

            // Rota al enemigo para que mire hacia el jugador
            transform.LookAt(player);

            // Reproduce el sonido de pasos si no se está reproduciendo ya
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
        else
        {
            // Si el enemigo no se está moviendo, detener el sonido de pasos
            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop();
            }
        }
    }
}
