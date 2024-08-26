using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    public Image healthBar; // La imagen de la barra de salud en la UI

    private PlayerHealth playerHealth; // Referencia al script PlayerHealth

    void Start()
    {
        // Encuentra el objeto del jugador y el script PlayerHealth asociado
        playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("No se encontró el script PlayerHealth en la escena.");
        }
    }

    void Update()
    {
        if (playerHealth != null && healthBar != null)
        {
            // Actualiza la barra de salud en la UI
            healthBar.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;
        }
    }
}
