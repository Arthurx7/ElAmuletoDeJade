using UnityEngine;

public class ComponenteVerde : MonoBehaviour
{
    public Bombillo bombilloEntrada1;
    public Bombillo bombilloEntrada2;
    public Bombillo bombilloSalida;
    
    void Update()
    {
        // Si ambas entradas son positivas, enviamos una carga positiva a la salida
        if (bombilloEntrada1.ObtenerEstadoCarga() && bombilloEntrada2.ObtenerEstadoCarga())
        {
            bombilloSalida.RecibirCarga(true);
        }
        else
        {
            bombilloSalida.RecibirCarga(false);
        }
    }
}
