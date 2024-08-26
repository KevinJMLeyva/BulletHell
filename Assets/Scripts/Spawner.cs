using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;          // El prefab del jefe que deseas spawnear
    public Transform spawnPoint;           // El punto de spawn del jefe
    public float spawnDelay = 10f;         // Tiempo en segundos entre spawns
    private bool bossSpawned = false;      // Flag para verificar si el jefe ya fue spawneado

    private Transform player;           // Transform del jugador

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnBoss", spawnDelay, spawnDelay);
    }

    void SpawnBoss()
    {
        if (!bossSpawned)
        {
            // Instanciar el jefe en el punto de spawn
            Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
            bossSpawned = true;  // Marcar como spawneado para evitar múltiples instancias
        }
    }
}
