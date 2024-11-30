using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    public float maxSanity = 100f;
    public float currentSanity;
    public UnityEngine.UI.Slider sanityBar;

    public SanityEffects sanityEffects;

    private void Start()
    {
        currentSanity = maxSanity;
    }

    public void LoseSanity(float amount)
    {
        currentSanity -= amount;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);

        // Actualiza la barra de cordura si existe
        if (sanityBar != null)
        {
            sanityBar.value = currentSanity / maxSanity;
        }

        // Llamar al script de efectos y pasar el porcentaje de cordura
        if (sanityEffects != null)
        {
            float sanityPercentage = currentSanity / maxSanity;
            sanityEffects.ApplySanityEffects(sanityPercentage);
        }

        CheckSanityEffects();
    }

    public void GainSanity(float amount)
    {
        currentSanity += amount;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        Debug.Log($"Ganaste {amount} de cordura. Cordura actual: {currentSanity}");
        // Actualizar barra de cordura y efectos si corresponde
        if (sanityBar != null) sanityBar.value = currentSanity / maxSanity;
        if (sanityEffects != null) sanityEffects.ApplySanityEffects(currentSanity / maxSanity);
    }


    private void CheckSanityEffects()
    {
        if (currentSanity <= 0)
        {
            
            Debug.Log("¡El jugador ha perdido toda la cordura!");
        }
        else if (currentSanity < maxSanity / 2)
        {
            
            Debug.Log("La cordura está disminuyendo, efectos activados.");
        }
    }
}
