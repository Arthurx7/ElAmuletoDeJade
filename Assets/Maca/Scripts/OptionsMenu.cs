using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject optionsPanel;    // Panel principal de opciones
    public GameObject helpPanel;       // Panel de ayuda

    // M�todo para abrir el men� de opciones desde la interfaz principal
    public void OpenOptionsMenu()
    {
        optionsPanel.SetActive(true);  // Activa el panel de opciones
        helpPanel.SetActive(false);    // Aseg�rate de que el panel de ayuda est� desactivado
    }

    // M�todo para cerrar el men� de opciones y regresar a la interfaz principal
    public void CloseOptionsMenu()
    {
        optionsPanel.SetActive(false); // Desactiva el panel de opciones
    }

    // M�todo para abrir el panel de ayuda desde el men� de opciones
    public void OpenHelpPanel()
    {
        optionsPanel.SetActive(false); // Desactiva el panel de opciones
        helpPanel.SetActive(true);     // Activa el panel de ayuda
    }

    // M�todo para regresar al men� de opciones desde el panel de ayuda
    public void BackToOptionsMenu()
    {
        helpPanel.SetActive(false);    // Desactiva el panel de ayuda
        optionsPanel.SetActive(true);  // Reactiva el panel de opciones
    }
}
