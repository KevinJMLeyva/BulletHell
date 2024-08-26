using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Panel de Game Over
    public AudioSource gameOverMusic; // Música de Game Over
    public AudioSource backgroundMusic; // Música de fondo

    public AudioSource steepSound; // Música de fondo


    void Start()
    {
        // Asegúrate de que el panel de Game Over esté desactivado al inicio
        gameOverPanel.SetActive(false);
        
        // Reproduce la música de fondo si está asignada
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    public void ShowGameOver()
    {
        // Muestra el panel de Game Over
        gameOverPanel.SetActive(true);

        // Detén la música de fondo
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        // Detén el sonido de los pasos
        if (steepSound != null && steepSound.isPlaying)
        {
            steepSound.Stop();
        }

        // Reproduce la música de Game Over si está asignada
        if (gameOverMusic != null && !gameOverMusic.isPlaying)
        {
            gameOverMusic.Play();
        }

        // Asegúrate de que el cursor sea visible y desbloqueado
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Pausa el juego
        Time.timeScale = 0f;
    }


    public void Retry()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Restablece el tiempo de juego
    }

    public void Quit()
    {
        // Sale del juego
        Application.Quit();
    }
}
