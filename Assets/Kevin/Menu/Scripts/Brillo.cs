using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Volume volume;               // Referencia al Volume global HDRP
    public Slider gammaSlider;          // Slider que controlar� el valor del gamma

    private LiftGammaGain gamma;        // El componente LiftGammaGain
    private float sliderValue;          // Variable para almacenar el valor del slider

    void Start()
    {
        // Obt�n el componente LiftGammaGain del volumen HDRP
        if (volume.profile.TryGet<LiftGammaGain>(out gamma))
        {
            // Si el slider est� asignado, agregar el listener
            if (gammaSlider != null)
            {
                gammaSlider.onValueChanged.AddListener(ChangeSlider);
                sliderValue = gammaSlider.value; // Establece el valor inicial del slider
                UpdateGamma(sliderValue);  // Establece el valor inicial del gamma
            }
        }
    }

    // Funci�n que cambia el valor del slider y lo pasa a la actualizaci�n del gamma
    public void ChangeSlider(float value)
    {
        sliderValue = Mathf.Clamp(value, -1f, 5f);  // Asegura que el valor est� entre 0.1 y 2
        Debug.Log("Slider Value Changed: " + sliderValue);  // Imprime el valor del slider
        UpdateGamma(sliderValue);  // Llama a la funci�n que actualiza el gamma con el nuevo valor
    }

    // M�todo que actualiza el gamma en tiempo real a trav�s del slider
    void UpdateGamma(float value)
    {
        if (gamma != null)
        {
            // Ajustar el valor de gamma (en el rango de 0.1 a 2)
            // Modificando solo el cuarto valor del Vector4 (gamma)
            gamma.gamma.value = new Vector4(0, 0, 0, value);  // Ajusta el gamma para R, G y B
            Debug.Log("Gamma Updated: " + value);  // Verifica si el gamma se actualiz�
        }
    }
}