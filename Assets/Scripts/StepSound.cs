using UnityEngine;


public class StepSound : MonoBehaviour
{
    public AudioSource footstepAudio;
    private bool isMoving;

    void Start()
    {
        footstepAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Detecta el movimiento del jugador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Verifica si hay movimiento
        isMoving = horizontal != 0 || vertical != 0;

        if (isMoving)
        {
            // Reproduce el sonido si no está ya reproduciéndose
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            // Detiene el sonido si no hay movimiento
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
    }
}
