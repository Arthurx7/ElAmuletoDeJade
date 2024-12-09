using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 40; // Vida máxima del jefe
    private int currentHealth; // Vida actual del jefe
    public Slider healthSlider; // Referencia al slider de vida

    void Start()
    {
        // Inicializa la vida del jefe al máximo y actualiza el slider
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisionó tiene el tag "Bullet"
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20); // Reduce 20 puntos de vida

            // Opcional: Destruir la bala después del impacto
            Destroy(other.gameObject);
        }
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Asegúrate de que la vida no sea menor a 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualiza la barra de vida
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // Verifica si la vida llega a 0
        if (currentHealth == 0)
        {
            Die(); // Llama al método para manejar la muerte del jefe
        }

        Debug.Log($"Jefe recibió daño. Vida actual: {currentHealth}");
    }

    private void Die()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3);
        Debug.Log("El jefe ha muerto.");
        Destroy(gameObject); // Destruye el objeto jefe
    }
}
