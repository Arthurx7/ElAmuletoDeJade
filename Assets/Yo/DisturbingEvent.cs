using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbingEvent : MonoBehaviour
{
    public float sanityLoss = 50f; // Cu�nto se pierde por presenciar el evento
    private bool eventTriggered = false; // Para asegurar que el evento no se repita
    public GameObject prefabToSpawn; // El prefab que se generar� y se mover�
    public float moveDuration = 1f; // Duraci�n del movimiento
    public Camera playerCamera; // La c�mara del jugador
    public float distanceFromCamera = 2f; // Distancia del objeto frente a la c�mara

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !eventTriggered)
        {
            // Llamar a la funci�n de p�rdida de cordura
            PlayerSanity playerSanity = other.GetComponent<PlayerSanity>();
            if (playerSanity != null)
            {
                playerSanity.LoseSanity(sanityLoss);
                eventTriggered = true; // Asegurarse de que no se active de nuevo

                // Instanciar el prefab y moverlo en la c�mara
                if (prefabToSpawn != null && playerCamera != null)
                {
                    // Crear el prefab y colocarlo al frente de la c�mara
                    GameObject spawnedObject = Instantiate(prefabToSpawn);
                    StartCoroutine(MoveAndDestroyObject(spawnedObject));
                }
            }
        }
    }

    // Coroutine para mover el prefab y destruirlo al final del movimiento
    IEnumerator MoveAndDestroyObject(GameObject spawnedObject)
    {
        float elapsedTime = 0f;

        // Establecer la posici�n inicial y final del prefab
        Vector3 screenStartPos = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, distanceFromCamera)); // Abajo, en el centro
        Vector3 screenEndPos = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, distanceFromCamera)); // Arriba, en el centro

        // Rotaci�n: mirar hacia la c�mara con ajuste de 180 grados
        Quaternion targetRotation = Quaternion.LookRotation(playerCamera.transform.forward) * Quaternion.Euler(0, 180, 0);

        // Aplicar la rotaci�n inicial
        spawnedObject.transform.rotation = targetRotation;

        while (elapsedTime < moveDuration)
        {
            // Mover el prefab de la posici�n inicial a la final
            spawnedObject.transform.position = Vector3.Lerp(screenStartPos, screenEndPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destruir el prefab al final del movimiento
        Destroy(spawnedObject);
    }
}
