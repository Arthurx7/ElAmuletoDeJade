using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 40; // Vida m�xima del zombie
    private int currentHealth; // Vida actual del zombie

    void Start()
    {
        // Inicializa la vida del zombie al m�ximo
        currentHealth = maxHealth;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto con el que colision� tiene el tag "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20); // Reduce 20 puntos de vida

            // Opcional: Destruir la bala despu�s del impacto
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Verifica si la vida llega a 0 o menos
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die(); // Llama al m�todo para manejar la muerte del zombie
        }

        Debug.Log($"Zombie recibi� da�o. Vida actual: {currentHealth}");
    }

    private void Die()
    {
        Debug.Log("Zombie ha muerto.");
        Destroy(gameObject); // Destruye el objeto zombie
    }
}
