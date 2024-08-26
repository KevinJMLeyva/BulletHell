using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This player controller class will update the camera to follow the player from a third-person perspective.
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    public Transform player; // El jugador que la cámara seguirá
    public float mouseSensitivity = 100.0f; // Sensibilidad del ratón
    public float distanceFromPlayer = 4.0f; // Distancia fija desde el jugador
    public float height = 2.0f; // Altura de la cámara sobre el jugador
    public float cameraTilt = -10.0f; // Inclinación de la cámara en grados (negativo para inclinación hacia abajo)
    private float xRotation = 0.0f; // Rotación acumulada en el eje X

    /// <summary>
    /// This method is called before the first frame update
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al centro de la pantalla
    }

    /// <summary>
    /// This method is called once per frame
    /// </summary>
    void LateUpdate()
    {
        // Movimiento de la cámara con el ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Aplicar rotación horizontal al jugador
        player.Rotate(Vector3.up * mouseX);

        // Mantener la rotación vertical fija
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotación vertical

        // Calcular la posición objetivo de la cámara
        Vector3 targetPosition = player.position - player.forward * distanceFromPlayer;
        targetPosition.y += height;

        // Crear la rotación de inclinación
        Quaternion tiltRotation = Quaternion.Euler(cameraTilt, 0f, 0f);

        // Aplicar la inclinación y hacer que la cámara mire al jugador
        transform.position = targetPosition;
        transform.rotation = tiltRotation * Quaternion.LookRotation(player.position - transform.position);
    }
}
