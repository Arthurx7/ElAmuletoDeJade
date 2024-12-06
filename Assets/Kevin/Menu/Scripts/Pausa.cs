using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public GameObject ObjectMenuPausa;
    public bool pausa = false;
    

    void Start()
    {
        ObjectMenuPausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausa == false)
            {
                ObjectMenuPausa.SetActive(true);
                
                pausa = true;

                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
                foreach (AudioSource audio in sonidos)
                {
                    if (audio.isPlaying) // Solo pausa los que están reproduciéndose
                    {
                        audio.Pause();
                    }
                }
            }
            else if (pausa == true)
            {
                Resumir();
            }
        }
    }

    public void Resumir()
    {
        ObjectMenuPausa.SetActive(false);
        pausa=false;

        Time.timeScale = 1f;
        Cursor.visible=false;
        Cursor.lockState = CursorLockMode.Locked;
        AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in sonidos)
        {
            if (!audio.isPlaying) // Solo reproduce los que estaban pausados
            {
                audio.Play();
            }
        }
    }

    public void Resumir2()
    {
        ObjectMenuPausa.SetActive(false);
        pausa = false;

        Time.timeScale = 1f;
        AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in sonidos)
        {
            if (!audio.isPlaying) // Solo reproduce los que estaban pausados
            {
                audio.Play();
            }
        }
    }

    public void CambiarMenu()
    {
        SceneManager.LoadScene(0);
        Resumir2();
    }




}
