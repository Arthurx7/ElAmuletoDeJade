using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Image bloodEffectImage; // Referencia a la imagen del efecto de sangre
    private int maxHealth = 100;
    private int currentHealth;
    private int damageAmount = 2;
    private int healAmountPerSecond = 2;
    private float timeSinceLastDamage = 0f;
    private float healDelay = 3f;
    private float healTimer = 0f;
    private bool inFireZone = false;
    private float fireDamageTimer = 0f;
    private int fireDamagePerSecond = 2; // Daño base por fuego
    private float fireDamageMultiplier = 1f; // Multiplicador del daño por fuego
    private float fireMultiplierIncreaseRate = 0.5f; // Velocidad de aumento del multiplicador
    private float maxFireDamageMultiplier = 5f; // Máximo multiplicador permitido
    private float bloodEffectDuration = 0.5f; // Duración del efecto de sangre
    private float bloodEffectFadeSpeed = 1f; // Velocidad de desvanecimiento del efecto

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        // Asegúrate de que la imagen del efecto de sangre esté inicialmente invisible
        if (bloodEffectImage != null)
        {
            var tempColor = bloodEffectImage.color;
            tempColor.a = 0f;
            bloodEffectImage.color = tempColor;
        }
    }

    void Update()
    {
        timeSinceLastDamage += Time.deltaTime;

        if (timeSinceLastDamage >= healDelay && !inFireZone)
        {
            healTimer += Time.deltaTime;

            if (healTimer >= 1f)
            {
                RegenerateHealth(healAmountPerSecond);
                healTimer = 0f;
            }
        }

        if (inFireZone)
        {
            fireDamageTimer += Time.deltaTime;

            // Incrementa el multiplicador de daño por fuego con el tiempo
            if (fireDamageMultiplier < maxFireDamageMultiplier)
            {
                fireDamageMultiplier += fireMultiplierIncreaseRate * Time.deltaTime;
            }

            if (fireDamageTimer >= 1f)
            {
                int progressiveDamage = Mathf.RoundToInt(fireDamagePerSecond * fireDamageMultiplier);
                TakeFireDamage(progressiveDamage);
                fireDamageTimer = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arma"))
        {
            TakeDamage();
        }

        if (other.CompareTag("Fuego"))
        {
            inFireZone = true;
            fireDamageMultiplier = 1f; // Reinicia el multiplicador al entrar en la zona de fuego
            Debug.Log("Jugador en zona de fuego. Daño progresivo activado.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fuego"))
        {
            inFireZone = false;
            fireDamageTimer = 0f;
            fireDamageMultiplier = 1f; // Reinicia el multiplicador al salir de la zona
            Debug.Log("Jugador salió de la zona de fuego. Daño progresivo desactivado.");
        }
    }

    public void TakeDamage()
    {
        currentHealth -= damageAmount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthSlider.value = currentHealth;
        timeSinceLastDamage = 0f;
        healTimer = 0f;

        // Activa el efecto de sangre
        if (bloodEffectImage != null)
        {
            StopAllCoroutines(); // Detiene cualquier corrutina de desvanecimiento en curso
            StartCoroutine(ShowBloodEffect());
        }

        if (currentHealth == 0)
        {
            Debug.Log("El personaje ha muerto.");
        }
    }

    private void TakeFireDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthSlider.value = currentHealth;
        timeSinceLastDamage = 0f;

        // Activa el efecto de sangre
        if (bloodEffectImage != null)
        {
            StopAllCoroutines(); // Detiene cualquier corrutina de desvanecimiento en curso
            StartCoroutine(ShowBloodEffect());
        }

        Debug.Log($"Recibiendo daño por fuego: -{amount} puntos. Vida actual: {currentHealth}");
    }

    private void RegenerateHealth(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            healthSlider.value = currentHealth;
            Debug.Log("Regenerando vida. Vida actual: " + currentHealth);
        }
    }

    private IEnumerator ShowBloodEffect()
    {
        // Muestra el efecto de sangre
        var tempColor = bloodEffectImage.color;
        tempColor.a = 1f;
        bloodEffectImage.color = tempColor;

        // Espera la duración del efecto
        yield return new WaitForSeconds(bloodEffectDuration);

        // Desvanece el efecto de sangre
        while (bloodEffectImage.color.a > 0)
        {
            tempColor.a -= Time.deltaTime * bloodEffectFadeSpeed;
            bloodEffectImage.color = tempColor;
            yield return null;
        }
    }
}
