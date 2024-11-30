using System.Collections;
using UnityEngine;

public class DisturbingEvent : MonoBehaviour
{
    public float sanityLoss = 50f; // Cantidad de cordura que se pierde por evento perturbador
    public float sanityGain = 30f; // Cantidad de cordura que se gana por evento positivo
    public GameObject prefabToSpawn; // Prefab que se generará y moverá
    public float moveDuration = 1f; // Duración del movimiento
    public Camera playerCamera; // Cámara del jugador
    public float distanceFromCamera = 2f; // Distancia frente a la cámara
    public AudioSource eventAudioSource; // AudioSource para el evento perturbador

    private PlayerSanity playerSanity; // Referencia al script de cordura del jugador
    private bool hasTriggered = false; // Bandera para controlar si el evento ya ocurrió

    private void Start()
    {
        playerSanity = FindObjectOfType<PlayerSanity>();
        if (playerSanity == null)
        {
            Debug.LogError("No se encontró el componente PlayerSanity en la escena.");
        }

        if (eventAudioSource == null)
        {
            Debug.LogError("No se asignó un AudioSource para el evento.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return; // Salir si ya se activó este evento

        if (other.CompareTag("Player"))
        {
            hasTriggered = true; // Marcar este evento como activado

            // Reproducir el sonido si no se ha reproducido antes
            if (eventAudioSource != null && !eventAudioSource.isPlaying)
            {
                eventAudioSource.Play();
            }

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

            // Desactivar el collider para evitar interacciones futuras
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
