using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody bulletRb;

    public float power = 100f;
    public float lifeTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.velocity = this.transform.forward * power;

        Destroy(this.gameObject, lifeTime); // Destruye la bala despu�s de lifeTime segundos
    }

    // Detectar colisiones
    void OnCollisionEnter(Collision collision)
    {
        // Aqu� puedes agregar m�s l�gica para interactuar con el objeto que golpeas
        Debug.Log("Hice da�o"); // Imprime el mensaje cuando la bala golpea algo

        Destroy(this.gameObject); // Destruye la bala cuando hace contacto
    }
}
