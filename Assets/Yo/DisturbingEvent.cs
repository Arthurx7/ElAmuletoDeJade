using System.Collections;
using UnityEngine;

public class DisturbingEvent : MonoBehaviour
{
    public float sanityLoss = 50f; // Cantidad de cordura que se pierde por evento
    public float sanityRegenAmount = 2f; // Cantidad que se regenera por segundo
    public float sanityRegenDelay = 3f; // Tiempo en segundos antes de comenzar la regeneración

    private float timeSinceLastEvent = 0f; // Tiempo desde el último evento perturbador

    public GameObject prefabToSpawn; // Prefab que se generará y moverá
    public float moveDuration = 1f; // Duración del movimiento
    public Camera playerCamera; // Cámara del jugador
    public float distanceFromCamera = 2f; // Distancia frente a la cámara

    private PlayerSanity playerSanity; // Referencia al script de cordura del jugador

    private void Start()
    {
        // Buscar automáticamente el script de PlayerSanity si no se asigna manualmente
        playerSanity = FindObjectOfType<PlayerSanity>();

        if (playerSanity == null)
        {
            Debug.LogError("No se encontró el componente PlayerSanity en la escena.");
        }
    }

    private void Update()
    {
        // Incrementa el tiempo desde el último evento
        timeSinceLastEvent += Time.deltaTime;

        // Comienza la regeneración de cordura si han pasado más de 3 segundos sin eventos
        if (timeSinceLastEvent >= sanityRegenDelay && playerSanity != null && playerSanity.currentSanity < playerSanity.maxSanity)
        {
            RegenerateSanity(sanityRegenAmount * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Llamar a la función de pérdida de cordura
            if (playerSanity != null)
            {
                playerSanity.LoseSanity(sanityLoss);
            }

            timeSinceLastEvent = 0f; // Reinicia el temporizador

            // Instanciar el prefab y moverlo en la cámara
            if (prefabToSpawn != null && playerCamera != null)
            {
                GameObject spawnedObject = Instantiate(prefabToSpawn);
                StartCoroutine(MoveAndDestroyObject(spawnedObject));
            }
        }
    }

    private void RegenerateSanity(float amount)
    {
        // Recuperar cordura usando el método de PlayerSanity
        if (playerSanity != null)
        {
            playerSanity.currentSanity += amount;
            playerSanity.currentSanity = Mathf.Clamp(playerSanity.currentSanity, 0, playerSanity.maxSanity);

            // Actualizar la barra de cordura
            if (playerSanity.sanityBar != null)
            {
                playerSanity.sanityBar.value = playerSanity.currentSanity / playerSanity.maxSanity;
            }

            // Aplicar efectos de cordura
            if (playerSanity.sanityEffects != null)
            {
                float sanityPercentage = playerSanity.currentSanity / playerSanity.maxSanity;
                playerSanity.sanityEffects.ApplySanityEffects(sanityPercentage);
            }

            Debug.Log($"Regenerando cordura: {amount}. Cordura actual: {playerSanity.currentSanity}");
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
