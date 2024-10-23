using UnityEngine;
using System.Collections;


public class BombilloFinalController : MonoBehaviour
{
    public Bombillo bombillo;          
    public GameObject[] lucesDeLaCasa; 
    public Transform teleportDestination; 
    public GameObject player;          
    public Animator screenTransition;  
    private bool lucesEncendidas = false;

    void Update()
    {
        if (bombillo.ObtenerEstadoCarga() && !lucesEncendidas)
        {
            ActivarLucesCasa(true);
            lucesEncendidas = true;

            StartCoroutine(TeleportWithTransition());
        }
        else if (!bombillo.ObtenerEstadoCarga() && lucesEncendidas)
        {
            ActivarLucesCasa(false);
            lucesEncendidas = false;
        }
    }

    private void ActivarLucesCasa(bool activar)
    {
        foreach (GameObject luzCasa in lucesDeLaCasa)
        {
            luzCasa.SetActive(activar);
        }
    }

    private IEnumerator TeleportWithTransition()
    {
        screenTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        player.transform.position = teleportDestination.position;

        screenTransition.SetTrigger("End");

        yield return new WaitForSeconds(1.5f); 

        player.GetComponent<MonoBehaviour>().enabled = true;  

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        screenTransition.enabled = false;

        yield return null;
    }
}
