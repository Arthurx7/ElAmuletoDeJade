using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private Color originalColor;

    void Start()
    {
        // Asigna la imagen del botón
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
    }

    // Cuando el mouse entra en el botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        Color hoverColor = originalColor;
        hoverColor.a = 0.2f; // Aumenta la opacidad
        buttonImage.color = hoverColor;
    }

    // Cuando el mouse sale del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        // Restablece el color original
        buttonImage.color = originalColor;
    }
}
