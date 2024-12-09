using UnityEngine;

public class Pausa : MonoBehaviour
{
    public GameObject ObjectMenuPausa;
    private bool pausa = false;

    void Start()
    {
        ObjectMenuPausa.SetActive(false);
        SetCursorState(false, CursorLockMode.Locked); // Asegúrate de que el cursor esté oculto al iniciar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausa)
            {
                Resumir();
            }
            else
            {
                Pausar();
            }
        }
    }

    private void Pausar()
    {
        ObjectMenuPausa.SetActive(true);
        pausa = true;

        Time.timeScale = 0f;
        SetCursorState(true, CursorLockMode.None);

        // Pausar todos los sonidos
        foreach (AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            if (audio.isPlaying)
            {
                audio.Pause();
            }
        }
    }

    public void Resumir()
    {
        ObjectMenuPausa.SetActive(false);
        pausa = false;

        Time.timeScale = 1f;
        SetCursorState(false, CursorLockMode.Locked);

        // Reanudar todos los sonidos
        foreach (AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            if (!audio.isPlaying)
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
        Resumir2(); // Asegúrate de reanudar el juego antes de cambiar al menú
        TransitionManager.Instance.LoadScene(TransitionManager.SCENE_NAME_MAIN_MENU);
    }

    private void SetCursorState(bool visible, CursorLockMode lockState)
    {
        Cursor.visible = visible;
        Cursor.lockState = lockState;
    }
}




   




