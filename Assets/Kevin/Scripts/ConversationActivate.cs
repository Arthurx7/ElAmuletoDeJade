using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationActivate : MonoBehaviour
{
    [SerializeField] private NPCConversation myconversation;
    public MonoBehaviour playerMovementScript;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Iniciar la conversaci�n
                ConversationManager.Instance.StartConversation(myconversation);

                // Desbloquear y mostrar el cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerMovementScript.enabled = false;
            }
        }
    }

    private void Update()
    {
        // Comprobar si la conversaci�n ha terminado
        if (!ConversationManager.Instance.IsConversationActive)
        {
            // Bloquear y ocultar el cursor cuando la conversaci�n termine
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerMovementScript.enabled = true;
        }
    }
}
