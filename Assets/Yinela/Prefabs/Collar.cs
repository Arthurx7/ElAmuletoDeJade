using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collar : MonoBehaviour
{
    public GameObject gameObjectToActivate;
    public GameObject gameObjectToActivate2;// El GameObject que deseas activar
    public GameObject gameObjectToDeactivate; // El GameObject que deseas desactivar

    public Recolectables recolectables;

    private bool isInTriggerZone = false; // Para saber si el jugador est� dentro del �rea del trigger

    void OnTriggerEnter(Collider other)
    {
        // Cuando el jugador entra en el trigger, activar la detecci�n de la tecla "E"
        if (other.CompareTag("Player")) // Aseg�rate de que el objeto que entra sea el jugador
        {
            isInTriggerZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Cuando el jugador sale del trigger, desactivar la detecci�n de la tecla "E"
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false;
        }
    }

    void Update()
    {
        // Si estamos en la zona del trigger y se presiona la tecla "E"
        if (isInTriggerZone && Input.GetKeyDown(KeyCode.F))
        {
            // Activar el GameObject y desactivar el otro
            if (gameObjectToActivate != null)
            {
                gameObjectToActivate.SetActive(true);
                gameObjectToActivate2.SetActive(true);
                recolectables.camandula = true;
            }

            if (gameObjectToDeactivate != null)
            {
                gameObjectToDeactivate.SetActive(false);
            }
        }
    }
}
