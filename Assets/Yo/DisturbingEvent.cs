using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbingEvent : MonoBehaviour
{
    public float sanityLoss = 50f; // Cuánto se pierde por presenciar el evento
    private bool eventTriggered = false; // Para asegurar que el evento no se repita
    public GameObject prefabToSpawn; // El prefab que se generará y se moverá
    public float moveDuration = 1f; // Duración del movimiento
    public Camera playerCamera; // La cámara del jugador
    public float distanceFromCamera = 2f; // Distancia del objeto frente a la cámara

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !eventTriggered)
        {
            // Llamar a la función de pérdida de cordura
            PlayerSanity playerSanity = other.GetComponent<PlayerSanity>();
            if (playerSanity != null)
            {
                playerSanity.LoseSanity(sanityLoss);
                eventTriggered = true; // Asegurarse de que no se active de nuevo

                // Instanciar el prefab y moverlo en la cámara
                if (prefabToSpawn != null && playerCamera != null)
                {
                    // Crear el prefab y colocarlo al frente de la cámara
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

        // Establecer la posición inicial y final del prefab
        Vector3 screenStartPos = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, distanceFromCamera)); // Abajo, en el centro
        Vector3 screenEndPos = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, distanceFromCamera)); // Arriba, en el centro

        // Rotación: mirar hacia la cámara con ajuste de 180 grados
        Quaternion targetRotation = Quaternion.LookRotation(playerCamera.transform.forward) * Quaternion.Euler(0, 180, 0);

        // Aplicar la rotación inicial
        spawnedObject.transform.rotation = targetRotation;

        while (elapsedTime < moveDuration)
        {
            // Mover el prefab de la posición inicial a la final
            spawnedObject.transform.position = Vector3.Lerp(screenStartPos, screenEndPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destruir el prefab al final del movimiento
        Destroy(spawnedObject);
    }
}
