using UnityEngine;

public class BotonMovimiento : MonoBehaviour
{
    public Camera mainCamera; // La cámara principal para detectar los clics
    public Ficha[] fichas; // Lista de fichas en el tablero que se pueden mover
    public Vector3Int gridSize; // Tamaño de la cuadrícula del tablero (en 3D)
    public Vector3Int moveDirection; // Dirección de movimiento definida desde el inspector

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detectar clic izquierdo del mouse
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Verificar si lo que se clicó es un plano de movimiento
                if (hit.collider.CompareTag("MovePlane"))
                {
                    // Verificar si hay fichas alineadas con el plano
                    foreach (Ficha ficha in fichas)
                    {
                        if (IsAligned(ficha.currentPos, hit.collider.transform.position))
                        {
                            ficha.MoveFicha(moveDirection, gridSize);
                        }
                    }
                }
            }
        }
    }

    // Verificar si la ficha está alineada con el plano clicado (en fila, columna o profundidad)
    private bool IsAligned(Vector3Int fichaPos, Vector3 hitPosition)
    {
        return Mathf.Approximately(fichaPos.x, hitPosition.x) ||
               Mathf.Approximately(fichaPos.y, hitPosition.y) ||
               Mathf.Approximately(fichaPos.z, hitPosition.z);
    }
}
