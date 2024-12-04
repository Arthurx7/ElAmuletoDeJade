using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

#if USING_HDRP
using UnityEngine.Rendering.HighDefinition;
#elif USING_URP
using UnityEngine.Rendering.Universal;
#endif

public class Brillo : MonoBehaviour
{
    public Volume volume;               // Referencia al Volume global HDRP
    public Slider gammaSlider;          // Slider que controlar� el valor del gamma

    private LiftGammaGain gamma;        // El componente LiftGammaGain
    private float sliderValue;          // Variable para almacenar el valor del slider

    void Start()
    {
        // Cargar el valor guardado de brillo desde PlayerPrefs
        sliderValue = PlayerPrefs.GetFloat("Brillo", 1f); // Valor por defecto: 1f

        // Configurar el slider con el valor cargado
        if (gammaSlider != null)
        {
            gammaSlider.value = sliderValue; // Establecer el valor inicial del slider
            gammaSlider.onValueChanged.AddListener(ChangeSlider); // Agregar el listener
        }

        // Obtener el componente LiftGammaGain del volumen HDRP
        if (volume.profile.TryGet<LiftGammaGain>(out gamma))
        {
            UpdateGamma(sliderValue); // Establecer el valor inicial del gamma
        }
    }

    // Funci�n que cambia el valor del slider y lo pasa a la actualizaci�n del gamma
    public void ChangeSlider(float value)
    {
        sliderValue = Mathf.Clamp(value, -1f, 5f); // Asegura que el valor est� entre -1 y 5
        PlayerPrefs.SetFloat("Brillo", sliderValue); // Guardar el valor del slider
        UpdateGamma(sliderValue); // Actualiza el gamma
    }

    // M�todo que actualiza el gamma en tiempo real a trav�s del slider
    void UpdateGamma(float value)
    {
        if (gamma != null)
        {
            // Ajustar el valor de gamma (solo el cuarto componente del Vector4)
            gamma.gamma.value = new Vector4(0, 0, 0, value); // Ajusta el gamma para R, G y B
        }
    }
}
