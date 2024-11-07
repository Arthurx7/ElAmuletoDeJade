using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class SceneLoader : MonoBehaviour
{
    // Este método se llama cuando se presiona el botón de "Nuevo Juego"
    public void LoadGameScene()
    {
        // Carga la escena llamada "Disparo"
        SceneManager.LoadScene("Disparo");
    }
}
