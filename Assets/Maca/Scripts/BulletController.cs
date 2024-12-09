using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody bulletRb;

    public float power = 100f;
    public float lifeTime = 4f;
    public int dano = 50;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.velocity = this.transform.forward * power;

        Destroy(this.gameObject, lifeTime); // Destruye la bala despu�s de lifeTime segundos
    }

    // Detectar colisiones
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
