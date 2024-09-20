using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewNote : MonoBehaviour
{
    public GameObject notaVisual;
    private bool activa;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && activa )
        {
            notaVisual.SetActive(true);

        }
        if(Input.GetKeyDown(KeyCode.Escape) && activa)
        {
            notaVisual.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            activa = true;
            Debug.Log("Colision con el jugador");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            activa = false;
            notaVisual.SetActive(false);
        }
    }
}
