using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Salud máxima del jugador
    public float currentHealth;    // Salud actual del jugador
    public GameOverManager gameOverManager; // Referencia al GameOverManager

    void Start()
    {
        currentHealth = maxHealth;  // Inicializa la salud actual al máximo
        if (gameOverManager != null)
        {
            gameOverManager.gameOverPanel.SetActive(false); // Asegúrate de que el panel de Game Over esté desactivado al inicio
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver(); // Llama a la función GameOver cuando la salud llega a cero
        }
    }

    void GameOver()
    {
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver(); // Llama al método ShowGameOver del GameOverManager
        }
    }
}
