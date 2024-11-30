using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light spotLight; // La luz que hará el efecto de parpadeo
    public float minIntensity = 0.5f; // Intensidad mínima de la luz
    public float maxIntensity = 2.0f; // Intensidad máxima de la luz
    public float flickerSpeed = 0.1f; // Velocidad del parpadeo
    public bool useRandomPattern = true; // Si el titileo debe ser aleatorio

    private float timer;

    void Start()
    {
        if (spotLight == null)
        {
            spotLight = GetComponent<Light>();
        }
    }

    void Update()
    {
        if (spotLight != null)
        {
            timer += Time.deltaTime;

            if (useRandomPattern)
            {
                // Parpadeo con variaciones aleatorias
                spotLight.intensity = Random.Range(minIntensity, maxIntensity);
            }
            else
            {
                // Parpadeo suave entre dos valores
                float flicker = Mathf.PingPong(timer * flickerSpeed, 1);
                spotLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, flicker);
            }
        }
    }
}
