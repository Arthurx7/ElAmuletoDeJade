using System.Collections;
using UnityEngine;
using TMPro; // Importar para TextMeshPro

public class WeaponController : MonoBehaviour
{
    public Transform shootSpawn;
    public GameObject bulletPrefab;

    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip fullReloadSound;  // Sonido completo de recarga al recargar todas las balas

    public int maxBullets = 6; // Máximo de balas en el tambor
    private int currentBullets;

    public float fullReloadTime = 2f; // Tiempo para recargar completamente
    public float fireRate = 3f; // Delay entre disparos en segundos

    private bool isReloading = false;
    private bool canShoot = true; // Controla si se puede disparar

    // Referencias a los elementos UI
    public TextMeshProUGUI bulletCountText; // Texto para mostrar el contador de balas
    public TextMeshProUGUI reloadPromptText; // Texto para mostrar "Presiona R para recargar"

    void Start()
    {
        currentBullets = maxBullets; // Comienza con todas las balas cargadas
        UpdateBulletUI(); // Actualiza el contador en la UI

        reloadPromptText.gameObject.SetActive(false); // Asegurarse de que el texto de recarga esté oculto al inicio
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Detectar disparo con clic izquierdo (Mouse0)
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot && !isReloading)
        {
            if (currentBullets > 0)
            {
                Shoot();
            }
            else
            {
                // Mostrar el texto de recarga y ocultar el contador de balas
                reloadPromptText.gameObject.SetActive(true);
                bulletCountText.gameObject.SetActive(false); // Ocultar contador de balas
                Debug.Log("No hay balas, recarga.");
            }
        }

        // Recargar todas las balas cuando se presiona 'R'
        if (Input.GetKeyDown(KeyCode.R) && currentBullets < maxBullets && !isReloading)
        {
            StartCoroutine(FullReload());
        }
    }

    void Shoot()
    {
        canShoot = false; // Impedir que dispare hasta que pase el tiempo del fireRate
        InstantiateBullet();
        currentBullets--; // Disminuir una bala cada vez que se dispara

        audioSource.PlayOneShot(shootSound); // Sonido del disparo
        UpdateBulletUI(); // Actualizar el contador de balas en la UI
        StartCoroutine(ShootingCooldown()); // Delay entre disparos
    }

    private void InstantiateBullet()
    {
        Instantiate(bulletPrefab, shootSpawn.position, shootSpawn.rotation);
    }

    // Recarga completa cuando se presiona 'R'
    IEnumerator FullReload()
    {
        isReloading = true;
        audioSource.PlayOneShot(fullReloadSound); // Sonido largo de recarga

        Debug.Log("Recargando el arma...");

        yield return new WaitForSeconds(fullReloadTime); // Tiempo de recarga completa

        currentBullets = maxBullets; // Recargar todas las balas
        UpdateBulletUI(); // Actualizar el contador de balas en la UI

        // Ocultar el texto de recarga y volver a mostrar el contador de balas
        reloadPromptText.gameObject.SetActive(false);
        bulletCountText.gameObject.SetActive(true);

        isReloading = false;
        Debug.Log("Arma recargada.");
    }

    // Cooldown entre disparos
    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(fireRate); // Esperar el tiempo definido en fireRate
        canShoot = true; // Permitir disparar de nuevo
    }

    // Actualiza el contador de balas en la UI
    void UpdateBulletUI()
    {
        bulletCountText.text = currentBullets + "/" + maxBullets;
    }
}
