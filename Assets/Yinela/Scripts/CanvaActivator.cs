using UnityEngine;

public class CanvasActivator : MonoBehaviour
{
    public GameObject canvasToActivate; // El Canvas que se activará

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que entra al trigger es el jugador
        {
            if (canvasToActivate != null)
            {
                canvasToActivate.SetActive(true); // Activa el Canvas
                Destroy(gameObject); // Elimina el objeto con el trigger para evitar que se active nuevamente
            }
        }
    }
}
