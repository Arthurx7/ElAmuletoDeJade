using UnityEngine;

public class Bombillo : MonoBehaviour
{
    public Light luz;            // Referencia a la luz del bombillo
    public bool estadoInicial = false; // Puedes definir si el bombillo empieza encendido o apagado

    private bool cargaActual;

    void Start()
    {
        // Inicializa el estado del bombillo basado en el valor de estadoInicial
        cargaActual = estadoInicial;
        ActualizarEstadoLuz();
    }

    public void RecibirCarga(bool carga)
    {
        // Actualiza el estado de carga y la luz del bombillo
        cargaActual = carga;
        ActualizarEstadoLuz();
    }

    private void ActualizarEstadoLuz()
    {
        // Activa o desactiva la luz según la carga actual
        luz.enabled = cargaActual;
    }

    public bool ObtenerEstadoCarga()
    {
        // Devuelve el estado actual de la carga (encendido o apagado)
        return cargaActual;
    }
}
