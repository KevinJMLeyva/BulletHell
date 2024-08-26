using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    public Text bulletText; // El texto UI que mostrará el conteo de balas
    private int bulletCount; // Contador de balas

    void Start()
    {
        bulletCount = 0;
        UpdateBulletText();
    }

    // Este método se llamará cuando se dispare una bala
    public void AddBullet()
    {
        bulletCount++;
        UpdateBulletText();
    }

    // Este método se llamará cuando una bala sea destruida
    public void RemoveBullet()
    {
        if (bulletCount > 0) // Asegúrate de que el contador no baje de cero
        {
            bulletCount--;
            UpdateBulletText();
        }
    }

    // Actualiza el texto del contador de balas
    void UpdateBulletText()
    {
        bulletText.text = "Bullets: " + bulletCount;
    }
}

