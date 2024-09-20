using UnityEngine;

public class BombilloFinalController : MonoBehaviour
{
    public Bombillo bombillo;          // Referencia al script del bombillo que controla la energía
    public GameObject[] lucesDeLaCasa; // Luces de la casa que se activarán
    private bool lucesEncendidas = false;

    void Update()
    {
        // Revisa continuamente el estado del bombillo
        if (bombillo.ObtenerEstadoCarga() && !lucesEncendidas)
        {
            // Si el bombillo está encendido y las luces aún no están activadas, activarlas
            ActivarLucesCasa(true);
            lucesEncendidas = true;
        }
        else if (!bombillo.ObtenerEstadoCarga() && lucesEncendidas)
        {
            // Si el bombillo está apagado y las luces están encendidas, apagarlas
            ActivarLucesCasa(false);
            lucesEncendidas = false;
        }
    }

    private void ActivarLucesCasa(bool activar)
    {
        // Activa o desactiva todas las luces de la casa
        foreach (GameObject luzCasa in lucesDeLaCasa)
        {
            luzCasa.SetActive(activar);
        }
    }
}
