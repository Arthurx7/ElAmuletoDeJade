using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float velocidad = 20f;
    public Rigidbody rb;
    public int dano = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * velocidad;
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            ZombieAI enemigo = collision.GetComponent<ZombieAI>();

        if (enemigo != null)
        {
            enemigo.tomarDano(dano);
            Destroy(gameObject);
        }

        }
    }
}

