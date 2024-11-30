using System.Collections;
using UnityEngine;

public class DisturbingEvent : MonoBehaviour
{
    public float sanityLoss = 50f; // Cantidad de cordura que se pierde por evento perturbador
    public float sanityGain = 30f; // Cantidad de cordura que se gana por evento positivo
    public GameObject prefabToSpawn; // Prefab que se generar� y mover�
    public float moveDuration = 1f; // Duraci�n del movimiento
    public Camera playerCamera; // C�mara del jugador
    public float distanceFromCamera = 2f; // Distancia frente a la c�mara

    private PlayerSanity playerSanity; // Referencia al script de cordura del jugador
    private bool hasTriggered = false; // Bandera para controlar si el evento ya ocurri�

    private void Start()
    {
        playerSanity = FindObjectOfType<PlayerSanity>();
        if (playerSanity == null)
        {
            Debug.LogError("No se encontr� el componente PlayerSanity en la escena.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return; // Salir si ya se activ� este evento

        if (other.CompareTag("Player"))
        {
            hasTriggered = true; // Marcar este evento como activado

            // Verificar si este objeto es un evento perturbador
            if (gameObject.CompareTag("DisturbingEvent"))
            {
                if (playerSanity != null)
                {
                    playerSanity.LoseSanity(sanityLoss);
                }

                // Instanciar el prefab perturbador si corresponde
                if (prefabToSpawn != null && playerCamera != null)
                {
                    GameObject spawnedObject = Instantiate(prefabToSpawn);
                    StartCoroutine(MoveAndDestroyObject(spawnedObject));
                }
            }

            // Verificar si este objeto es un evento positivo
            if (gameObject.CompareTag("PositiveEvent"))
            {
                if (playerSanity != null)
                {
                    playerSanity.GainSanity(sanityGain);
                }
            }

            // Opcional: Desactivar el collider para evitar interacciones futuras
            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator MoveAndDestroyObject(GameObject spawnedObject)
    {
        float elapsedTime = 0f;

        Vector3 screenStartPos = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, distanceFromCamera));
        Vector3 screenEndPos = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, distanceFromCamera));

        Quaternion targetRotation = Quaternion.LookRotation(playerCamera.transform.forward) * Quaternion.Euler(0, 180, 0);
        spawnedObject.transform.rotation = targetRotation;

        while (elapsedTime < moveDuration)
        {
            spawnedObject.transform.position = Vector3.Lerp(screenStartPos, screenEndPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(spawnedObject);
    }
}
