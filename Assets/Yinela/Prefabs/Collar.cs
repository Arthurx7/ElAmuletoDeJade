using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collar : MonoBehaviour
{
    public GameObject gameObjectToActivate;
    public GameObject gameObjectToActivate2;// El GameObject que deseas activar
    public GameObject gameObjectToDeactivate; // El GameObject que deseas desactivar

    private bool isInTriggerZone = false; // Para saber si el jugador está dentro del área del trigger

    void OnTriggerEnter(Collider other)
    {
        // Cuando el jugador entra en el trigger, activar la detección de la tecla "E"
        if (other.CompareTag("Player")) // Asegúrate de que el objeto que entra sea el jugador
        {
            isInTriggerZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Cuando el jugador sale del trigger, desactivar la detección de la tecla "E"
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false;
        }
    }

    void Update()
    {
        // Si estamos en la zona del trigger y se presiona la tecla "E"
        if (isInTriggerZone && Input.GetKeyDown(KeyCode.E))
        {
            // Activar el GameObject y desactivar el otro
            if (gameObjectToActivate != null)
            {
                gameObjectToActivate.SetActive(true);
                gameObjectToActivate2.SetActive(true);
            }

            if (gameObjectToDeactivate != null)
            {
                gameObjectToDeactivate.SetActive(false);
            }
        }
    }
}
