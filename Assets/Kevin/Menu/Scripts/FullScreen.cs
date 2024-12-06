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
        // Desactiva temporalmente el listener del Toggle para evitar conflictos
        toggle.onValueChanged.RemoveAllListeners();

        // Cargar el estado de pantalla completa desde PlayerPrefs
        bool isFullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1; // Predeterminado: pantalla completa activada
        toggle.isOn = isFullScreen;
        Screen.fullScreen = isFullScreen;

        // Reactiva el listener del Toggle y asocia el método
        toggle.onValueChanged.AddListener(ActivarPantallaCompleta);

        // Configura las resoluciones en el dropdown
        RevisarResolucion();
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
        PlayerPrefs.SetInt("FullScreen", pantallaCompleta ? 1 : 0);
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
            // Filtra solo las resoluciones con formato rectangular (16:9, 16:10, etc.)
            float aspectRatio = (float)resolutions[i].width / resolutions[i].height;
            if (Mathf.Abs(aspectRatio - 16f / 9f) < 0.1f || Mathf.Abs(aspectRatio - 16f / 10f) < 0.1f)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                // Verifica cuál es la resolución actual
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentresolution = options.Count - 1; // Usa el índice del filtro
                }
            }
        }

        // Añade las opciones al dropdown
        resolutionsdropdown.AddOptions(options);

        // Configura el valor actual del dropdown según PlayerPrefs
        int savedResolution = PlayerPrefs.GetInt("numberresolution", currentresolution);
        resolutionsdropdown.value = savedResolution;
        resolutionsdropdown.RefreshShownValue();

        // Aplica la resolución guardada
        CambiarResolucion(savedResolution);
    }


    public void CambiarResolucion(int indiceResolucion)
    {
        // Guarda el índice de la resolución seleccionada
        PlayerPrefs.SetInt("numberresolution", indiceResolucion);

        // Cambia la resolución
        Resolution resolution = resolutions[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
