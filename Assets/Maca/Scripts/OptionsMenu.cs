using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject optionsPanel;    // Panel principal de opciones
    public GameObject helpPanel;       // Panel de ayuda

    // Método para abrir el menú de opciones desde la interfaz principal
    public void OpenOptionsMenu()
    {
        optionsPanel.SetActive(true);  // Activa el panel de opciones
        helpPanel.SetActive(false);    // Asegúrate de que el panel de ayuda esté desactivado
    }

    // Método para cerrar el menú de opciones y regresar a la interfaz principal
    public void CloseOptionsMenu()
    {
        optionsPanel.SetActive(false); // Desactiva el panel de opciones
    }

    // Método para abrir el panel de ayuda desde el menú de opciones
    public void OpenHelpPanel()
    {
        optionsPanel.SetActive(false); // Desactiva el panel de opciones
        helpPanel.SetActive(true);     // Activa el panel de ayuda
    }

    // Método para regresar al menú de opciones desde el panel de ayuda
    public void BackToOptionsMenu()
    {
        helpPanel.SetActive(false);    // Desactiva el panel de ayuda
        optionsPanel.SetActive(true);  // Reactiva el panel de opciones
    }
}
