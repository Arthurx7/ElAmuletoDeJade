using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbingEvent : MonoBehaviour
{
    public float sanityLoss = 50f; // Cuánto se pierde por presenciar el evento
    private bool eventTriggered = false; // Para asegurar que el evento no se repita

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
            }
        }
    }
}
