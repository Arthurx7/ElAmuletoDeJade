using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SanityEffects : MonoBehaviour
{
    public Volume postProcessVolume; // Referencia al Volume en la escena
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        // Asegúrate de que el perfil de post-procesado contiene los efectos
        if (postProcessVolume.profile.TryGet(out vignette) &&
            postProcessVolume.profile.TryGet(out chromaticAberration))
        {
            // Inicializar los efectos con valores predeterminados si es necesario
            vignette.intensity.value = 0.1f; // Valor inicial para la viñeta
            chromaticAberration.intensity.value = 0f; // Valor inicial para aberración cromática
            Debug.Log("Efectos inicializados correctamente.");
        }
        else
        {
            Debug.LogError("No se encontraron los efectos de Vignette o Chromatic Aberration en el perfil de post-procesado.");
        }
    }

    public void ApplySanityEffects(float sanityPercentage)
    {
        // Verificar que los efectos estén disponibles antes de modificarlos
        if (vignette != null && chromaticAberration != null)
        {
            // A medida que la cordura baja, aumentamos los efectos
            // Modificar los rangos de intensidad para aumentar el impacto visual
            vignette.intensity.value = Mathf.Lerp(0.1f, 0.75f, 1 - sanityPercentage); // Mayor intensidad máxima
            chromaticAberration.intensity.value = Mathf.Lerp(0.0f, 1.5f, 1 - sanityPercentage); // Supera el valor máximo normal para intensificar el efecto

            // Puedes experimentar con valores aún más altos para ver el cambio visual

            Debug.Log($"Aplicando efectos: Viñeta = {vignette.intensity.value}, Aberración cromática = {chromaticAberration.intensity.value}");
        }
        else
        {
            Debug.LogError("Los efectos no están inicializados correctamente.");
        }
    }
}
