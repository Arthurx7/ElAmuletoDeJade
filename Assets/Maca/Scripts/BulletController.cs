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

        Destroy(this.gameObject, lifeTime); // Destruye la bala después de lifeTime segundos
    }

    // Detectar colisiones
    void OnCollisionEnter(Collision collision)
    {
        // Aquí puedes agregar más lógica para interactuar con el objeto que golpeas
        Debug.Log("Hice daño"); // Imprime el mensaje cuando la bala golpea algo

        Destroy(this.gameObject); // Destruye la bala cuando hace contacto
    }
}
