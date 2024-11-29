using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttoms : MonoBehaviour
{
    public AudioSource fuente;
    public AudioClip clip;
    void Start()
    {
        fuente.clip = clip;
    }

    public void Reproducir()
    {
        fuente.Play();
    }
}