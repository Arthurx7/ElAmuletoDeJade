using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 270f; // �ngulo al que se abrir� la puerta (en Z)
    public float closedAngle = 180f; // �ngulo cuando la puerta est� cerrada (en Z)
    public float speed = 2f; // Velocidad de apertura/cierre

    private bool isPlayerInTrigger = false; // Verifica si el jugador est� en el trigger
    private bool isOpen = false; // Estado de la puerta (abierta/cerrada)
    private float currentAngle; // �ngulo actual de la puerta en Z
    private float targetAngle; // �ngulo objetivo de la puerta en Z

    void Start()
    {
        // Inicializa la puerta en posici�n cerrada
        isOpen = false;
        currentAngle = closedAngle;
        targetAngle = closedAngle;

        // Establece expl�citamente la rotaci�n inicial
        transform.rotation = Quaternion.Euler(-90f, 0f, currentAngle);
    }

    void Update()
    {
        // Comprueba si el jugador presiona 'E' dentro del trigger
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen; // Cambia el estado de la puerta
            targetAngle = isOpen ? openAngle : closedAngle;
        }

        // Interpola suavemente el �ngulo actual hacia el �ngulo objetivo
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * speed);

        // Mantiene la rotaci�n fija en X y Y, y cambia solo Z
        transform.rotation = Quaternion.Euler(-90f, 0f, currentAngle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
