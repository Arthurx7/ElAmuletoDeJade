using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown resolutionsdropdown;
    private Resolution[] resolutions;

    void Start()
    {
        // Configura el estado del toggle de pantalla completa
        toggle.isOn = Screen.fullScreen;

        // Configura las resoluciones en el dropdown
        RevisarResolucion();
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void RevisarResolucion()
    {
        // Obtén todas las resoluciones disponibles
        resolutions = Screen.resolutions;
        Debug.Log("Número de resoluciones detectadas: " + resolutions.Length);

        // Limpia las opciones del dropdown
        resolutionsdropdown.ClearOptions();

        // Crea la lista de resoluciones
        List<string> options = new List<string>();
        int currentresolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Verifica cuál es la resolución actual
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentresolution = i;
            }
        }

        // Añade las opciones al dropdown
        resolutionsdropdown.AddOptions(options);

        // Configura el valor actual del dropdown según PlayerPrefs o la resolución actual
        int savedResolution = PlayerPrefs.GetInt("numberresolution", currentresolution);
        resolutionsdropdown.value = savedResolution;
        resolutionsdropdown.RefreshShownValue();
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        // Guarda el índice de la resolución seleccionada
        PlayerPrefs.SetInt("numberresolution", resolutionsdropdown.value);

        // Cambia la resolución
        Resolution resolution = resolutions[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
