using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Image bloodEffectImage; // Referencia a la imagen del efecto de sangre
    private int maxHealth = 100;
    private int currentHealth;
    private int damageAmount = 5;
    private int healAmountPerSecond = 2;
    private float timeSinceLastDamage = 0f;
    private float healDelay = 3f;
    private float healTimer = 0f;
    private bool inFireZone = false;
    private float fireDamageTimer = 0f;
    private int fireDamagePerSecond = 4; // Da�o base por fuego
    private float fireDamageMultiplier = 2f; // Multiplicador del da�o por fuego
    private float fireMultiplierIncreaseRate = 0.5f; // Velocidad de aumento del multiplicador
    private float maxFireDamageMultiplier = 5f; // M�ximo multiplicador permitido
    private float bloodEffectDuration = 0.5f; // Duraci�n del efecto de sangre
    private float bloodEffectFadeSpeed = 1f; // Velocidad de desvanecimiento del efecto

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        // Aseg�rate de que la imagen del efecto de sangre est� inicialmente invisible
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

            // Incrementa el multiplicador de da�o por fuego con el tiempo
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
            Debug.Log("Jugador en zona de fuego. Da�o progresivo activado.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fuego"))
        {
            inFireZone = false;
            fireDamageTimer = 0f;
            fireDamageMultiplier = 1f; // Reinicia el multiplicador al salir de la zona
            Debug.Log("Jugador sali� de la zona de fuego. Da�o progresivo desactivado.");
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
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(2);
            Debug.Log("El personaje ha muerto.");
        }
    }

    private void TakeFireDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (currentHealth == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(2);
            Debug.Log("El personaje ha muerto.");
        }

        healthSlider.value = currentHealth;
        timeSinceLastDamage = 0f;

        // Activa el efecto de sangre
        if (bloodEffectImage != null)
        {
            StopAllCoroutines(); // Detiene cualquier corrutina de desvanecimiento en curso
            StartCoroutine(ShowBloodEffect());
        }

        Debug.Log($"Recibiendo da�o por fuego: -{amount} puntos. Vida actual: {currentHealth}");
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

        // Espera la duraci�n del efecto
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
