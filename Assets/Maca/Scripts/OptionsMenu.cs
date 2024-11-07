using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel; // Asigna el panel desde la jerarqu�a

    public void OpenOptionsMenu()
    {
        // Activa el panel de opciones
        optionsPanel.SetActive(true);

        // Deshabilita los elementos detr�s
        Time.timeScale = 0f; // Esto se usa com�nmente para pausar el juego
    }

    public void CloseOptionsMenu()
    {
        // Cierra el panel de opciones
        optionsPanel.SetActive(false);

        // Reactiva los elementos detr�s
        Time.timeScale = 1f; // Esto restaura la escala de tiempo al valor normal
    }
}
