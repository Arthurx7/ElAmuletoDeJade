using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel; // Asigna el panel desde la jerarquía

    public void OpenOptionsMenu()
    {
        // Activa el panel de opciones
        optionsPanel.SetActive(true);

        // Deshabilita los elementos detrás
        Time.timeScale = 0f; // Esto se usa comúnmente para pausar el juego
    }

    public void CloseOptionsMenu()
    {
        // Cierra el panel de opciones
        optionsPanel.SetActive(false);

        // Reactiva los elementos detrás
        Time.timeScale = 1f; // Esto restaura la escala de tiempo al valor normal
    }
}
