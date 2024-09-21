using UnityEngine;

public class Ficha : MonoBehaviour
{
    public Vector3Int currentPos; // Posición actual de la ficha en la cuadrícula
    public float moveSpeed = 5f; // Velocidad de movimiento de la ficha
    public Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f); // Escala del tablero

    // Mueve la ficha en la dirección indicada, teniendo en cuenta la escala
    public void MoveFicha(Vector3Int direction, Vector3Int gridSize)
    {
        Vector3 targetPosition = transform.position + Vector3.Scale((Vector3)direction, scale); // Ajustar al tamaño de la escala
        StartCoroutine(MoveToPosition(targetPosition));
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; // Asegurarse que termina exactamente en la posición correcta
    }
}
