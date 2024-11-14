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

                // Activar el cursor
                Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
                Cursor.visible = true; // Muestra el cursor
            }
        }
    }

    private void Update()
    {
        // Comprobar si la conversación ha terminado
        if (!ConversationManager.Instance.IsConversationActive)
        {
            // Desactivar el cursor cuando la conversación termine
            Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
            Cursor.visible = false; // Oculta el cursor
        }
    }
}
