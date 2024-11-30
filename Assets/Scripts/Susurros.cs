using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Susurros : MonoBehaviour
{
    private AudioSource susurroAudio;
    private bool haSusurrado = false;

    void Start()
    {
        // Obtiene el componente AudioSource adjunto al GameObject
        susurroAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra en colisión tiene el tag "Player" y si aún no se ha reproducido el susurro
        if (other.CompareTag("Player") && !haSusurrado)
        {
            susurroAudio.Play();
            haSusurrado = true; // Marca el susurro como reproducido
        }
    }
}
