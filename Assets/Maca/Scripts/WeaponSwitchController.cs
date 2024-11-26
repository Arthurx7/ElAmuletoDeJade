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
            UnlockCursor(); // Desbloquear el cursor al activar las manos
        }

        // Detectar la tecla 2 para cambiar al arma
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateWeapon();
            LockCursor(); // Bloquear el cursor al activar el arma
        }
    }

    private void ActivateHands()
    {
        manos.SetActive(true);
        canvasManos.SetActive(true);
        arma.SetActive(false);
        canvasArma.SetActive(false);
    }

    private void ActivateWeapon()
    {
        arma.SetActive(true);
        canvasArma.SetActive(true);
        manos.SetActive(false);
        canvasManos.SetActive(false);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}