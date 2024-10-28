using System.Collections;
using UnityEngine;

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

    void Start()
    {
        currentBullets = maxBullets; // Comienza con todas las balas cargadas
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
                Debug.Log("recarga el arma no hay balas");
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
        StartCoroutine(ShortReload()); // Sonido corto de recarga tras cada disparo
        StartCoroutine(ShootingCooldown()); // Delay entre disparos
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
        audioSource.PlayOneShot(fullReloadSound); // Sonido largo de recarga

        Debug.Log("recargando el arma");

        yield return new WaitForSeconds(fullReloadTime); // Tiempo de recarga completa

        currentBullets = maxBullets; // Recargar todas las balas
        isReloading = false;

        Debug.Log("arma recargada");
    }

    // Cooldown entre disparos
    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(fireRate); // Esperar el tiempo definido en fireRate (4 segundos en este caso)
        canShoot = true; // Permitir disparar de nuevo
    }
}
