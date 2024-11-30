using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;
using DialogueEditor;

public class WeaponController : MonoBehaviour
{
    public Transform shootSpawn; // Boquilla del arma
    public GameObject bulletPrefab;
    public GameObject muzzleFlashEffect; // Efecto de flash al disparar

    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip shortReloadSound;
    public AudioClip fullReloadSound;

    public int maxBullets = 6;
    private int currentBullets;

    public float fullReloadTime = 2f;
    public float shortReloadTime = 0.5f;
    public float fireRate = 3f;

    public float recoilForce = 4f;

    private bool isReloading = false;
    private bool canShoot = true;

    private Vector3 initialPosition;

    void Start()
    {
        currentBullets = maxBullets;
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (ConversationManager.Instance.IsConversationActive) return;
        //if (Cursor.lockState == CursorLockMode.None) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * 5f);

        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot && !isReloading)
        {
            if (currentBullets > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("Recarga el arma, no hay balas");
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentBullets < maxBullets && !isReloading)
        {
            StartCoroutine(FullReload());
        }
    }

    void Shoot()
    {
        canShoot = false;

        // Crear efecto de flash
        GameObject flashClone = Instantiate(muzzleFlashEffect, shootSpawn.position, shootSpawn.rotation);
        Destroy(flashClone, 1f);

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Vector3 shootDirection;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            shootDirection = (hit.point - shootSpawn.position).normalized;
        }
        else
        {
            shootDirection = Camera.main.transform.forward;
        }

        InstantiateBullet(shootDirection);
        currentBullets--;

        AddRecoil();
        audioSource.PlayOneShot(shootSound);

        StartCoroutine(ShortReload());
        StartCoroutine(ShootingCooldown());
    }

    private void InstantiateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, shootSpawn.position, Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 0f, 0f));

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.velocity = direction * 100f;
        }
    }

    private void AddRecoil()
    {
        transform.localPosition -= new Vector3(0, 0, recoilForce / 50f);
        transform.localRotation *= Quaternion.Euler(-recoilForce, 0f, 0f);
    }

    IEnumerator ShortReload()
    {
        isReloading = true;
        audioSource.PlayOneShot(shortReloadSound);
        yield return new WaitForSeconds(shortReloadTime);
        isReloading = false;
    }

    IEnumerator FullReload()
    {
        isReloading = true;

        audioSource.PlayOneShot(fullReloadSound);
        Debug.Log("Recargando el arma");
        yield return new WaitForSeconds(fullReloadTime);
        currentBullets = maxBullets;

        isReloading = false;
        Debug.Log("Arma recargada");
    }

    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
