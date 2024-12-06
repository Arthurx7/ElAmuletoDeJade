using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionPuerta : MonoBehaviour
{
    public Animator anim;
    public LockControl activado;
    private bool isAnimationFinished = false; // Flag para saber si la animación terminó

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activado!= null && activado.Rotacion)
        {
            anim.SetTrigger("OpenChest"); // Activar el trigger en el Animator
            activado.Rotacion = false; // Evitar que se active repetidamente
        }
    }
}
