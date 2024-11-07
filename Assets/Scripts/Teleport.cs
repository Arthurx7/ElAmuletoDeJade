using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    public Transform targetPosition; // La posici�n a donde se teletransportar� el jugador
    public Animator screenTransition; // Animador para la transici�n visual
    private bool playerInZone = false; // Para verificar si el jugador est� dentro del �rea de teletransporte

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    private IEnumerator TeleportPlayer()
    {
        // Inicia la animaci�n de oscurecimiento
        if (screenTransition != null)
        {
            screenTransition.SetTrigger("FadeOut");
        }

        // Espera el tiempo necesario para la animaci�n de oscurecimiento
        yield return new WaitForSeconds(1f);

        // Mueve al jugador a la posici�n objetivo
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && targetPosition != null)
        {
            player.transform.position = targetPosition.position;
        }

        // Inicia la animaci�n de aclarado
        if (screenTransition != null)
        {
            screenTransition.SetTrigger("FadeIn");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true; // El jugador est� dentro del �rea
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false; // El jugador sali� del �rea
        }
    }
}
