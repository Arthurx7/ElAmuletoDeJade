using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbingEvent : MonoBehaviour
{
    public float sanityLoss = 50f; // Cu�nto se pierde por presenciar el evento
    private bool eventTriggered = false; // Para asegurar que el evento no se repita

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
            }
        }
    }
}
