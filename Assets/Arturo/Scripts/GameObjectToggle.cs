using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectToggle : MonoBehaviour
{
    public Light referenceLight; // Referencia al componente Light
    public List<GameObject> objectsToActivate; // Lista de GameObjects a activar
    public List<GameObject> objectsToDeactivate; // Lista de GameObjects a desactivar

    void Update()
    {
        // Comprueba si el componente Light está activo
        if (referenceLight != null && referenceLight.enabled)
        {
            // Activa todos los GameObjects en la lista "objectsToActivate"
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                    obj.SetActive(true);
            }

            // Desactiva todos los GameObjects en la lista "objectsToDeactivate"
            foreach (GameObject obj in objectsToDeactivate)
            {
                if (obj != null)
                    obj.SetActive(false);
            }
        }
        else
        {
            // Si el Light no está activo, invierte la acción
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                    obj.SetActive(false);
            }

            foreach (GameObject obj in objectsToDeactivate)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }
    }
}
