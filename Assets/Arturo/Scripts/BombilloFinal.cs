using UnityEngine;

public class BombilloFinalController : MonoBehaviour
{
    public Bombillo bombillo;          // Referencia al script del bombillo que controla la energía
    public GameObject[] lucesDeLaCasa; // Luces de la casa que se activarán
    public GameObject[] objetosDeSusto; // Objetos de susto que se activarán/desactivarán
    private bool lucesEncendidas = false;

    void Update()
    {
        if (bombillo.ObtenerEstadoCarga() && !lucesEncendidas)
        {
            ConfigurarEstadoCasa(true, false);
            lucesEncendidas = true;
        }
        else if (!bombillo.ObtenerEstadoCarga() && lucesEncendidas)
        {
            ConfigurarEstadoCasa(false, true);
            lucesEncendidas = false;
        }
    }

    private void ConfigurarEstadoCasa(bool lucesEstado, bool sustosEstado)
    {
        // Configura las luces de la casa
        foreach (GameObject luzCasa in lucesDeLaCasa)
        {
            luzCasa.SetActive(lucesEstado);
        }

        // Configura los sustos de la casa
        foreach (GameObject objetoSusto in objetosDeSusto)
        {
            objetoSusto.SetActive(sustosEstado);
        }
    }
}
