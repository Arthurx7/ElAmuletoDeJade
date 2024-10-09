using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class SceneLoader : MonoBehaviour
{
    // Este m�todo se llama cuando se presiona el bot�n de "Nuevo Juego"
    public void LoadGameScene()
    {
        // Carga la escena llamada "Disparo"
        SceneManager.LoadScene("Disparo");
    }
}
