using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchController : MonoBehaviour
{
    public GameObject arma; // Referencia al GameObject del arma
    public GameObject canvasArma; // Referencia al Canvas del arma
    public GameObject manos; // Referencia al GameObject de las manos
    public GameObject canvasManos; // Referencia al Canvas de las manos

    void Start()
    {
        // Asegurarnos de que al iniciar solo las manos estén activas
        ActivateHands();
    }

    void Update()
    {
        // Detectar la tecla 1 para cambiar a las manos
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateHands();
        }

        // Detectar la tecla 2 para cambiar al arma
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateWeapon();
        }
    }

    // Método para activar las manos
    private void ActivateHands()
    {
        manos.SetActive(true); // Activar las manos
        canvasManos.SetActive(true); // Activar el Canvas de las manos

        arma.SetActive(false); // Desactivar el arma
        canvasArma.SetActive(false); // Desactivar el Canvas del arma
    }

    // Método para activar el arma
    private void ActivateWeapon()
    {
        arma.SetActive(true); // Activar el arma
        canvasArma.SetActive(true); // Activar el Canvas del arma

        manos.SetActive(false); // Desactivar las manos
        canvasManos.SetActive(false); // Desactivar el Canvas de las manos
    }
}
