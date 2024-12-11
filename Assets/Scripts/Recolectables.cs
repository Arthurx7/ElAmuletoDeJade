using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para manejar las imágenes

public class Recolectables : MonoBehaviour
{
    // Referencias a las imágenes
    public Image cartaImage;
    public Image aguaImage;
    public Image rosarioImage;

    public bool camandula = false;

    private int total;

    private void OnTriggerStay(Collider other)
    {
        // Verificar si el jugador está dentro del trigger
        if (other.CompareTag("Player"))
        {
            // Detectar si se presiona la tecla E
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Validar la etiqueta del objeto en el que está este script
                switch (gameObject.tag)
                {
                    case "Carta":
                        SetImageOpacity(cartaImage, 1f); // Cruz
                        total++;
                        gameObject.SetActive(false);
                        break;
                    case "Agua":
                        SetImageOpacity(aguaImage, 1f); // Carta
                        total++;
                        //Destroy(gameObject);
                         gameObject.SetActive(false);
                        break;
                }
            }
        }
    }
    void Update() {
        if(camandula)
        {
            SetImageOpacity(rosarioImage, 1f); // Cruz
            total++;
            camandula=false;
        }

        if(total==3)
        {
            Debug.Log("Total de objetos recolectados");
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

