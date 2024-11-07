using UnityEngine;
using UnityEngine.UI; // Necesario para usar sliders.

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;  // El slider que representa la vida del personaje.
    private int maxHealth = 100; // La vida m�xima del personaje.
    private int currentHealth;   // La vida actual del personaje.
    private int damageAmount = 5; // La cantidad de da�o por colisi�n.

    void Start()
    {
        // Inicializa la vida del personaje.
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        // Reduce la vida si colisiona con el objeto que tiene el collider.
        TakeDamage();
    }

    public void TakeDamage()
    {
        currentHealth -= damageAmount;

        // Evitar que la vida baje de 0.
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Actualiza el slider.
        healthSlider.value = currentHealth;

        // Si la vida llega a 0, puedes manejar la muerte del personaje aqu�.
        if (currentHealth == 0)
        {
            Debug.Log("El personaje ha muerto.");
            // Agrega l�gica adicional, como desactivar el personaje o mostrar un mensaje de muerte.
        }
    }
}
