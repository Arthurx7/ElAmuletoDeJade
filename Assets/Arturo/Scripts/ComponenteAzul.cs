using UnityEngine;

public class ComponenteAzul : MonoBehaviour
{
    public Bombillo bombilloEntrada1;
    public Bombillo bombilloEntrada2;
    public Bombillo bombilloSalida;

    void Update()
    {
        // Si una entrada es positiva y la otra es negativa, enviamos positivo a la salida
        if ((bombilloEntrada1.ObtenerEstadoCarga() && !bombilloEntrada2.ObtenerEstadoCarga()) ||
            (!bombilloEntrada1.ObtenerEstadoCarga() && bombilloEntrada2.ObtenerEstadoCarga()))
        {
            bombilloSalida.RecibirCarga(true);
        }
        else
        {
            bombilloSalida.RecibirCarga(false);
        }
    }
}
