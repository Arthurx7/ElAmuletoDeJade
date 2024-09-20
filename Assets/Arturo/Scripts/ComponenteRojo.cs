using UnityEngine;

public class ComponenteRojo : MonoBehaviour
{
    public Bombillo bombilloEntrada;
    public Bombillo bombilloSalida;

    void Update()
    {
        // Invertimos la carga de la entrada y la enviamos a la salida
        if (bombilloEntrada.ObtenerEstadoCarga())
        {
            bombilloSalida.RecibirCarga(false);  // Entrada positiva -> Salida negativa
        }
        else
        {
            bombilloSalida.RecibirCarga(true);   // Entrada negativa -> Salida positiva
        }
    }
}
