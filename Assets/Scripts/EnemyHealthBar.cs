using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBarFill;
    public Vector3 offset = new Vector3(0, 2, 0); // Desplazamiento de la barra respecto a la cabeza del enemigo
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (enemy == null)
        {
            enemy = GetComponent<Enemy>();
        }
        
        if (enemy != null)
        {
            healthBarFill.fillAmount = enemy.health / enemy.maxHealth;
        }
    }

}
