using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 0.0f;
    public float horizontalInput;
    public float forwardInput;
    public float jumpInput;
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey; // Tecla que permite cambiar entre cámaras
    public string inputId;
    
    public float mouseSensitivity = 100.0f; // Sensibilidad del ratón
    private float xRotation = 0.0f; // Rotación acumulada en el eje X

    Animator anim;
    public AudioSource footstepAudio; // AudioSource para los pasos


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al centro de la pantalla
    }

    void Update()
    {
        // Entrada de movimiento
        horizontalInput = Input.GetAxis("Horizontal" + inputId);
        forwardInput = Input.GetAxis("Vertical" + inputId);
        jumpInput = Input.GetAxis("Jump");

        Vector3 direction = new Vector3(horizontalInput, 0, forwardInput).normalized;
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetFloat("Move", 1f, 0.1f, Time.deltaTime); // Activa la animación de baile
        }
        // Movimiento del jugador
        if (direction.magnitude >= 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Mover hacia adelante y atrás
                transform.Translate(Vector3.forward * Time.deltaTime * speed * 1.5f * forwardInput);
                
                // Mover hacia la izquierda y derecha
                transform.Translate(Vector3.right * Time.deltaTime * speed * 1.5f * horizontalInput);
                anim.SetFloat("Move", 0.5f, 0.1f, Time.deltaTime);
            }
            else
            {
                // Mover hacia adelante y atrás
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
                
                // Mover hacia la izquierda y derecha
                transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
                anim.SetFloat("Move", 0.25f, 0.1f, Time.deltaTime);
            }

            // Comenzar/reproducir el sonido de pasos cuando el jugador se mueva
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            anim.SetFloat("Move", 0f, 0.1f, Time.deltaTime);

            // Detener el sonido de pasos cuando el jugador no se mueva
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }

        // Cambio de cámaras
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }

        // Movimiento de la cámara como en un FPS
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotación vertical

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
