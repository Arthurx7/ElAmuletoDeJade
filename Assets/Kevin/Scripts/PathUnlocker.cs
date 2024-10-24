using UnityEngine;

public class PathUnlocker : MonoBehaviour
{
    public GameObject objectToDisable;
    public GameObject objectToDisable2;

    // Este método será llamado cuando se seleccione la opción del diálogo
    public void DisableObject()
    {
        objectToDisable.SetActive(false);
        objectToDisable2.SetActive(false);
    }
}
