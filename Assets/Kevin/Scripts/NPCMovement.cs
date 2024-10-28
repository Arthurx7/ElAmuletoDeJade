using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform pointA; // Punto inicial del NPC (puede ser la posici�n actual)
    public Transform pointB; // Punto al que quieres que se mueva el NPC
    public float speed = 3f; // Velocidad de movimiento
    public Animator npcAnimator; // Referencia al Animator del NPC
    public GameObject npcGameObject; // El GameObject del NPC que se activar�
    private bool playerInRange = false; // Para verificar si el jugador est� en el trigger
    private bool isMoving = false; // Para verificar si el NPC ya est� en movimiento

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // El jugador est� dentro del trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // El jugador ha salido del trigger
        }
    }

    private void Update()
    {
        // Verificar si el jugador est� en rango y presion� la tecla E
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            // Activar el NPC cuando se presione la tecla E
            npcGameObject.SetActive(true);

            // Mover al NPC de A a B
            StartCoroutine(MoveToPointB());
        }
    }

    // Coroutine para mover al NPC de A a B
    IEnumerator MoveToPointB()
    {
        isMoving = true;

        // Enviar el booleano true al Animator para activar la animaci�n de caminar
        npcAnimator.SetBool("Walk", true);

        float step = speed * Time.deltaTime; // Cantidad a mover en cada frame

        // Mover el NPC hasta que alcance el punto B
        while (Vector3.Distance(npcGameObject.transform.position, pointB.position) > 0.1f)
        {
            npcGameObject.transform.position = Vector3.MoveTowards(npcGameObject.transform.position, pointB.position, step);
            yield return null; // Esperar hasta el siguiente frame
        }

        // Detener la animaci�n de caminar cuando llegue al punto B
        npcAnimator.SetBool("Walk", false);

        isMoving = false; // El NPC ha llegado al punto B
    }
}
