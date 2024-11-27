using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;  // Referencia al TextMeshProUGUI del bot�n
    public Color hoverColor = Color.red;  // Color que tomar� el texto cuando el cursor pase por encima
    private Color originalColor;  // Para almacenar el color original del texto

    void Start()
    {
        // Guardamos el color original del texto
        if (buttonText != null)
        {
            originalColor = buttonText.color;
        }
    }

    // M�todo que se llama cuando el cursor entra en el �rea del bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = hoverColor;  // Cambiamos el color a rojo
        }
    }

    // M�todo que se llama cuando el cursor sale del �rea del bot�n
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = originalColor;  // Restauramos el color original
        }
    }
}
