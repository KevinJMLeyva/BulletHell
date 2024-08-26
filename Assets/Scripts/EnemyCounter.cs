using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    public Text enemyCountText;  // Asigna el Text de UI desde el inspector
    private static EnemyCounter instance; // Referencia estática a la instancia actual
    private int enemyCount = 0; // Contador de enemigos

    void Start()
    {
        // Inicializa el contador en el inicio
        UpdateEnemyCountText();
    }

    public static void IncrementEnemyCount()
    {
        if (instance != null)
        {
            instance.enemyCount++;
            instance.UpdateEnemyCountText();
        }
    }

    public static void DecrementEnemyCount()
    {
        if (instance != null)
        {
            instance.enemyCount--;
            instance.UpdateEnemyCountText();
        }
    }

    private void UpdateEnemyCountText()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemies: " + enemyCount;
        }
    }

    private void Awake()
    {
        // Asegúrate de que solo haya una instancia del contador
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
