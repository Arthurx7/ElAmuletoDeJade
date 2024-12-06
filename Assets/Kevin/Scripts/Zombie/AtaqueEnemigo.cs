using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEnemigo : MonoBehaviour
{
    public PlayerHealth ScriptPlayer;
      
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider Colli){
        if(Colli.CompareTag("Player")){
            ScriptPlayer.TakeDamage();
            Debug.Log("Daño recibido");
        }
    }
}

