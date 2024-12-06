using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigos : MonoBehaviour
{
    public Animator Ani;
    public ZombieAI Enemigo;

    void OnTriggerEnter(Collider Coll){
        if(Coll.CompareTag("Player")){
            if(!Enemigo.stuneado){
                Ani.SetBool("Walk",false);
                Ani.SetBool("Run",false);
                Ani.SetBool("Atack",true);
                GetComponent<CapsuleCollider>().enabled = false;

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}