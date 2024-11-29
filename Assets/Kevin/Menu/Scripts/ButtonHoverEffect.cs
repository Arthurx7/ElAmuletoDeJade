using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;  // Referencia al TextMeshProUGUI del botón
    public Color hoverColor = Color.red;  // Color que tomará el texto cuando el cursor pase por encima
    private Color originalColor;  // Para almacenar el color original del texto

    void Start()
    {
        // Guardamos el color original del texto
        if (buttonText != null)
        {
            originalColor = buttonText.color;
        }
    }

    // Método que se llama cuando el cursor entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = hoverColor;  // Cambiamos el color a rojo
        }
    }

    // Método que se llama cuando el cursor sale del área del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = originalColor;  // Restauramos el color original
        }
    }
}
