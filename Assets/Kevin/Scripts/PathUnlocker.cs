using UnityEngine;

public class PathUnlocker : MonoBehaviour
{
    public GameObject objectToDisable;
    public GameObject objectToDisable2;

    // Este m�todo ser� llamado cuando se seleccione la opci�n del di�logo
    public void DisableObject()
    {
        objectToDisable.SetActive(false);
        objectToDisable2.SetActive(false);
    }
}
