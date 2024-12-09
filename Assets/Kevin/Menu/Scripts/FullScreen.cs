using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    public Toggle toggle;
    

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
        
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
        PlayerPrefs.SetInt("FullScreen", pantallaCompleta ? 1 : 0);
    }

    
}
