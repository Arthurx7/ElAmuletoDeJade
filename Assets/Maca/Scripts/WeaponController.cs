using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Para imágenes del canvas
using TMPro; // Para TextMesh Pro

public class WeaponController : MonoBehaviour
{
    public Transform shootSpawn;
    public GameObject bulletPrefab;

    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip shortReloadSound; // Sonido corto de recarga tras cada disparo
    public AudioClip fullReloadSound;  // Sonido completo de recarga al recargar todas las balas

    public int maxBullets = 6; // Máximo de balas en el tambor
    private int currentBullets;

    public float fullReloadTime = 2f; // Tiempo para recargar completamente
    public float shortReloadTime = 0.5f; // Tiempo de recarga tras cada disparo
    public float fireRate = 3f; // Delay entre disparos en segundos

    private bool isReloading = false;
    private bool canShoot = true; // Controla si se puede disparar

    public Image[] bulletImages; // Arreglo de imágenes de balas en el canvas
    public TMP_Text reloadText; // Texto de TextMesh Pro para mostrar "Presiona 'R' para recargar"

    void Start()
    {
        currentBullets = maxBullets; // Comienza con todas las balas cargadas
        reloadText.gameObject.SetActive(false); // Oculta el texto al iniciar
        UpdateBulletImages(); // Asegura que las imágenes coincidan con las balas actuales
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
                Debug.Log("Recarga el arma, no hay balas");
                ShowReloadText(); // Mostrar el texto cuando no haya balas
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

        UpdateBulletImages(); // Actualizar las imágenes de las balas

        audioSource.PlayOneShot(shootSound); // Sonido del disparo
        StartCoroutine(ShortReload()); // Sonido corto de recarga tras cada disparo
        StartCoroutine(ShootingCooldown()); // Delay entre disparos

        // Mostrar el texto inmediatamente si las balas llegan a 0
        if (currentBullets <= 0)
        {
            ShowReloadText(); // Mostrar el texto "Presiona 'R' para recargar"
        }
    }

    private void InstantiateBullet()
    {
        Instantiate(bulletPrefab, shootSpawn.position, shootSpawn.rotation);
    }

    // Recarga corta tras cada disparo
    IEnumerator ShortReload()
    {
        isReloading = true;
        audioSource.PlayOneShot(shortReloadSound); // Sonido corto de recarga

        yield return new WaitForSeconds(shortReloadTime); // Espera el tiempo corto de recarga

        isReloading = false;
    }

    // Recarga completa cuando se presiona 'R'
    IEnumerator FullReload()
    {
        isReloading = true;
        HideReloadText(); // Ocultar el texto de recarga
        audioSource.PlayOneShot(fullReloadSound); // Sonido largo de recarga

        Debug.Log("Recargando el arma");

        yield return new WaitForSeconds(fullReloadTime); // Tiempo de recarga completa

        currentBullets = maxBullets; // Recargar todas las balas
        UpdateBulletImages(); // Reactivar todas las imágenes de balas
        isReloading = false;

        Debug.Log("Arma recargada");
    }

    // Cooldown entre disparos
    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(fireRate); // Esperar el tiempo definido en fireRate
        canShoot = true; // Permitir disparar de nuevo
    }

    // Actualiza las imágenes del canvas para que coincidan con las balas restantes
    private void UpdateBulletImages()
    {
        for (int i = 0; i < bulletImages.Length; i++)
        {
            if (i < currentBullets)
            {
                bulletImages[i].enabled = true; // Mostrar la imagen
            }
            else
            {
                bulletImages[i].enabled = false; // Ocultar la imagen
            }
        }
    }

    // Mostrar el texto de recarga
    private void ShowReloadText()
    {
        reloadText.gameObject.SetActive(true); // Activa el texto en el canvas
        reloadText.text = "Presiona 'R' para recargar"; // Actualiza el texto (si es necesario)
    }

    // Ocultar el texto de recarga
    private void HideReloadText()
    {
        reloadText.gameObject.SetActive(false); // Desactiva el texto
    }
}
