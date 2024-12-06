using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para manejar las imágenes

public class Recolectables : MonoBehaviour
{
    // Referencias a las imágenes
    public Image camandulaImage;
    public Image cruzImage;
    public Image cartaImage;

    private void OnTriggerStay(Collider other)
    {
        // Verificar si el jugador está dentro del trigger
        if (other.CompareTag("Player"))
        {
            // Detectar si se presiona la tecla E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Validar la etiqueta del objeto en el que está este script
                switch (gameObject.tag)
                {
                    case "Camandula":
                        SetImageOpacity(camandulaImage, 1f); // Camándula
                        Destroy(gameObject);
                        break;
                    case "Cruz":
                        SetImageOpacity(cruzImage, 1f); // Cruz
                        break;
                    case "Carta":
                        SetImageOpacity(cartaImage, 1f); // Carta
                        break;
                }
            }
        }
    }

    // Método para establecer la opacidad de una imagen
    private void SetImageOpacity(Image image, float opacity)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = opacity;
            image.color = color;
        }
    }
}

