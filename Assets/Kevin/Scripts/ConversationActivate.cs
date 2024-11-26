using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationActivate : MonoBehaviour
{
    [SerializeField] private NPCConversation myconversation;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Iniciar la conversación
                ConversationManager.Instance.StartConversation(myconversation);

                // Desbloquear y mostrar el cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void Update()
    {
        // Comprobar si la conversación ha terminado
        if (!ConversationManager.Instance.IsConversationActive)
        {
            // Bloquear y ocultar el cursor cuando la conversación termine
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
