using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperInteraction : MonoBehaviour
{   
    public GameObject paper; // Objeto del papel
    public Camera playerCamera; // Cámara del jugador en primera persona
    public float interactionDistance = 2f; // Distancia máxima para la interacción
    public float rotationSpeed = 100f; // Velocidad de rotación del papel
    public float zoomSpeed = 2f; // Velocidad de zoom
    public float paperDistance = 0.5f; // Distancia a la que el papel se coloca frente al jugador
    public MonoBehaviour cameraController; // El script que controla la cámara (por ejemplo, FirstPersonController)

    private bool isHoldingPaper = false; // Si el jugador está sosteniendo el papel
    private bool isRotating = false; // Si el jugador está rotando el papel

    private Vector3 originalPosition; // Posición original del papel
    private Quaternion originalRotation; // Rotación original del papel
    private Vector3 originalScale; // Escala original del papel

    private float currentZoom = 0f; // Valor actual del zoom

    void Start()
    {
        // Guardar la posición, rotación y escala originales del papel al inicio
        originalPosition = paper.transform.position;
        originalRotation = paper.transform.rotation;
        originalScale = paper.transform.localScale;
    }

    void Update()
    {
        if (!isHoldingPaper)
        {
            // Detectar si el jugador está apuntando al objeto y está dentro del rango
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Emitir un rayo desde el centro de la pantalla (mira)
            RaycastHit hit;

            // **Dibujar el Raycast en la Scene**
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green); // Línea verde para el Raycast

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.transform.gameObject)
                {
                    Debug.Log("Presiona E para interactuar");
                }

                if (hit.transform.gameObject == paper && Input.GetKeyDown(KeyCode.E))
                {
                    // Si está apuntando al papel y presiona 'E', tomar el papel
                    PickupPaper();
                }
            }
        }
        else
        {
            // Si ya está sosteniendo el papel
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Si presiona 'E' nuevamente, devolver el papel
                ReturnPaper();
            }

            // Permitir rotar solo si se mantiene el clic izquierdo
            if (Input.GetMouseButtonDown(0))
            {
                isRotating = true; // Inicia la rotación al hacer clic
            }

            if (Input.GetMouseButtonUp(0))
            {
                isRotating = false; // Detener la rotación al soltar el clic
            }

            if (isRotating)
            {
                RotatePaper(); // Rotar solo si se está clicando y arrastrando
            }

            // Hacer zoom con la rueda del mouse
            ZoomPaper();
        }
    }

    private void PickupPaper()
    {
        isHoldingPaper = true;

        // Desactivar el control de la cámara
        if (cameraController != null)
        {
            cameraController.enabled = false; // Desactivar el script que controla la cámara
        }

        // Mover el papel frente al jugador
        paper.transform.position = playerCamera.transform.position + playerCamera.transform.forward * paperDistance;
        //paper.transform.rotation = playerCamera.transform.rotation; // Alinear la rotación con la cámara

        Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor para interactuar
        Cursor.visible = true; // Mostrar el cursor
    }

    private void ReturnPaper()
    {
        isHoldingPaper = false;

        // Devolver el papel a su posición, rotación y escala originales
        paper.transform.position = originalPosition;
        paper.transform.rotation = originalRotation;
        paper.transform.localScale = originalScale; // Restaurar la escala original

        // Restaurar el control de la cámara
        if (cameraController != null)
        {
            cameraController.enabled = true; // Reactivar el script de control de la cámara
        }

        // Bloquear nuevamente el cursor del ratón
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Ocultar el cursor
    }

    private void RotatePaper()
    {
        float rotateX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotateY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Rotación horizontal y vertical del papel
        paper.transform.Rotate(playerCamera.transform.up, -rotateX, Space.World); // Rotación en eje Y
        paper.transform.Rotate(playerCamera.transform.right, rotateY, Space.World); // Rotación en eje X
    }

    private void ZoomPaper()
    {
        float zoomChange = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom += zoomChange;

        // Limitar el zoom dentro de un rango específico
        currentZoom = Mathf.Clamp(currentZoom, -0.5f, 1f);

        // Cambiar el tamaño del papel basado en el zoom
        paper.transform.localScale = originalScale * (1f + currentZoom); // Ajustar en relación a la escala original
    }
}
