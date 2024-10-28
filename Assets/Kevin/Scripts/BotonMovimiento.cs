using System.Collections.Generic;
using UnityEngine;

public class BotonMovimiento : MonoBehaviour
{
    public GameObject tablero; // Objeto del tablero en la escena
    public float rayDistance = 7f; // Distancia del raycast
    public string pieceTag = "Ficha"; // Tag para identificar las fichas
    public float moveDistance = 1f; // Distancia de un movimiento
    private Vector3 boardMin; // Coordenadas m�nimas del tablero
    private Vector3 boardMax; // Coordenadas m�ximas del tablero

    // A�adido: Referencias a los triggers
    public Transform trigger1; // Primer trigger
    public Transform trigger2; // Segundo trigger
    public AudioSource audioSource;
    public AudioSource audioSource1;
    void Start()
    {
        // Calcular los l�mites del tablero basado en el Renderer o Collider
        Renderer tableroRenderer = tablero.GetComponent<Renderer>();
        if (tableroRenderer != null)
        {
            boardMin = tableroRenderer.bounds.min;
            boardMax = tableroRenderer.bounds.max;
        }
        else
        {
            Collider tableroCollider = tablero.GetComponent<Collider>();
            if (tableroCollider != null)
            {
                boardMin = tableroCollider.bounds.min;
                boardMax = tableroCollider.bounds.max;
            }
            else
            {
                Debug.LogError("El tablero no tiene un Renderer ni un Collider asignado.");
            }
        }
    }

    void OnMouseDown()
    {
        audioSource1.Play();
        // Mover fichas en todas las direcciones, verificando si pueden moverse
        MovePiecesInDirection(Vector3.right, Vector3.left);
        MovePiecesInDirection(Vector3.left, Vector3.right);
        MovePiecesInDirection(Vector3.forward, Vector3.back);
        MovePiecesInDirection(Vector3.back, Vector3.forward);
    }

    void MovePiecesInDirection(Vector3 rayDirection, Vector3 moveDirection)
    {
        // Raycast para detectar todas las fichas en la direcci�n del rayo
        RaycastHit[] hits = Physics.RaycastAll(transform.position, rayDirection, rayDistance);

        // Ordenar las fichas detectadas bas�ndonos en la direcci�n del movimiento
        List<RaycastHit> sortedHits = new List<RaycastHit>(hits);
        sortedHits.Sort((a, b) =>
            Vector3.Dot(a.transform.position, rayDirection).CompareTo(Vector3.Dot(b.transform.position, rayDirection)));

        // Iterar sobre todas las fichas detectadas y moverlas
        Vector3 lastOccupiedPosition = Vector3.zero; // Mantiene la posici�n de la �ltima ficha colocada
        bool completed = false; // Para verificar si ambas fichas llegaron a los triggers

        foreach (RaycastHit hit in sortedHits)
        {
            if (hit.collider.CompareTag(pieceTag))
            {
                GameObject assignedPiece = hit.collider.gameObject;

                // Encontrar la posici�n m�s lejana a la que puede moverse
                Vector3 targetPosition = FindFurthestPosition(assignedPiece.transform.position, rayDirection, moveDirection);

                // Si ya hay una ficha en la �ltima posici�n, mover a la pen�ltima
                if (lastOccupiedPosition != Vector3.zero && targetPosition == lastOccupiedPosition)
                {
                    // Mover a la posici�n anterior disponible
                    targetPosition -= moveDirection * moveDistance;
                }

                // Mover la ficha a la nueva posici�n si est� dentro de los l�mites
                if (IsWithinBoardLimits(targetPosition))
                {
                    assignedPiece.transform.position = targetPosition;
                    lastOccupiedPosition = targetPosition; // Actualizar la �ltima posici�n ocupada

                    // Comprobar si la ficha ha llegado a un trigger
                    if (Vector3.Distance(assignedPiece.transform.position, trigger1.position) < 0.1f ||
                        Vector3.Distance(assignedPiece.transform.position, trigger2.position) < 0.1f)
                    {
                        completed = true; // Marca como completado si llega a un trigger
                    }
                }
            }
        }

        // Verifica si ambas fichas est�n en los triggers
        if (completed && AreAllPiecesInTriggers())
        {
            audioSource.Play();
            Debug.Log("Completado"); // Mensaje en la consola
        }
    }

    // Nueva funci�n para comprobar si ambas fichas est�n en los triggers
    bool AreAllPiecesInTriggers()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag(pieceTag);
        foreach (GameObject piece in pieces)
        {
            if (!IsInTrigger(piece.transform.position))
            {
                return false; // Si alguna ficha no est� en un trigger, retorna false
            }
        }
        return true; // Todas las fichas est�n en los triggers
    }

    // Funci�n para comprobar si una posici�n est� en un trigger
    bool IsInTrigger(Vector3 position)
    {
        return (Vector3.Distance(position, trigger1.position) < 0.1f ||
                Vector3.Distance(position, trigger2.position) < 0.1f);
    }

    Vector3 FindFurthestPosition(Vector3 startPosition, Vector3 rayDirection, Vector3 moveDirection)
    {
        Vector3 currentPosition = startPosition;
        Vector3 lastValidPosition = currentPosition; // Posici�n m�s lejana alcanzable

        while (true)
        {
            Vector3 nextPosition = currentPosition + moveDirection * moveDistance;

            // Verificar si la nueva posici�n est� dentro de los l�mites del tablero
            if (!IsWithinBoardLimits(nextPosition))
            {
                break; // No puede moverse m�s all� del l�mite del tablero
            }

            // Verificar si hay otra ficha en la posici�n objetivo
            if (IsPositionOccupied(nextPosition))
            {
                break; // Detener el movimiento si hay otra ficha
            }

            // Actualizar la posici�n actual y marcarla como v�lida
            lastValidPosition = nextPosition;
            currentPosition = nextPosition;
        }

        return lastValidPosition; // Devolver la posici�n m�s lejana alcanzable
    }

    bool IsWithinBoardLimits(Vector3 position)
    {
        // Verificar si la ficha est� dentro de los l�mites del tablero
        return (position.x >= boardMin.x && position.x <= boardMax.x) &&
               (position.z >= boardMin.z && position.z <= boardMax.z);
    }

    bool IsPositionOccupied(Vector3 position)
    {
        // Realizar una verificaci�n en la posici�n objetivo para ver si hay una ficha
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f); // Detectar objetos en un radio peque�o
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(pieceTag))
            {
                return true; // Hay una ficha en la posici�n
            }
        }
        return false;
    }
}
