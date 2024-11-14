using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    public Transform targetPosition; // La posición a donde se teletransportará el jugador
    public Animator screenTransition; // Animador para la transición visual
    private bool playerInZone = false; // Para verificar si el jugador está dentro del área de teletransporte

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    private IEnumerator TeleportPlayer()
    {
        // Inicia la animación de oscurecimiento
        if (screenTransition != null)
        {
            screenTransition.SetTrigger("FadeOut");
        }

        // Espera el tiempo necesario para la animación de oscurecimiento
        yield return new WaitForSeconds(1f);

        // Mueve al jugador a la posición objetivo
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && targetPosition != null)
        {
            player.transform.position = targetPosition.position;
        }

        // Inicia la animación de aclarado
        if (screenTransition != null)
        {
            screenTransition.SetTrigger("FadeIn");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true; // El jugador está dentro del área
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false; // El jugador salió del área
        }
    }
}
