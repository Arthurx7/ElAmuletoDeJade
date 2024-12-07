using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionPuerta : MonoBehaviour
{
    public Animator anim;
    public LockControl activado;
    public GameObject rosario; // Objeto que será activado

    private bool animationFinished = false; // Bandera para controlar el estado de la animación

    // Start is called before the first frame update
    void Start()
    {
        if (rosario != null)
        {
            rosario.SetActive(false); // Asegurarse de que esté inactivo al inicio
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activado != null && activado.Rotacion)
        {
            anim.SetTrigger("OpenChest"); // Activar el trigger en el Animator
            activado.Rotacion = false; // Evitar que se active repetidamente
        }

        if (animationFinished)
        {
            // Desactivar el objeto actual y activar el rosario
            gameObject.SetActive(false);

            if (rosario != null)
            {
                rosario.SetActive(true);
            }

            animationFinished = false; // Resetear la bandera para evitar activaciones no deseadas
        }
    }

    // Este método será llamado desde el evento de la animación
    public void OnAnimationEnd()
    {
        animationFinished = true;
    }
}

