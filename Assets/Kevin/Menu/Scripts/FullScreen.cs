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
        // Obt�n todas las resoluciones disponibles
        resolutions = Screen.resolutions;
        Debug.Log("N�mero de resoluciones detectadas: " + resolutions.Length);

        // Limpia las opciones del dropdown
        resolutionsdropdown.ClearOptions();

        // Crea la lista de resoluciones
        List<string> options = new List<string>();
        int currentresolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Verifica cu�l es la resoluci�n actual
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentresolution = i;
            }
        }

        // A�ade las opciones al dropdown
        resolutionsdropdown.AddOptions(options);

        // Configura el valor actual del dropdown seg�n PlayerPrefs o la resoluci�n actual
        int savedResolution = PlayerPrefs.GetInt("numberresolution", currentresolution);
        resolutionsdropdown.value = savedResolution;
        resolutionsdropdown.RefreshShownValue();
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        // Guarda el �ndice de la resoluci�n seleccionada
        PlayerPrefs.SetInt("numberresolution", resolutionsdropdown.value);

        // Cambia la resoluci�n
        Resolution resolution = resolutions[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
