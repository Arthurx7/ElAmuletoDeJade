using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; // Importa EventSystems para detectar interacciones con la UI
using System.Collections;
using DialogueEditor;

public class WeaponController : MonoBehaviour
{
    public Transform shootSpawn;
    public GameObject bulletPrefab;

    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip shortReloadSound;
    public AudioClip fullReloadSound;

    public int maxBullets = 6;
    private int currentBullets;

    public float fullReloadTime = 2f;
    public float shortReloadTime = 0.5f;
    public float fireRate = 3f;

    private bool isReloading = false;
    private bool canShoot = true;

    public Image[] bulletImages;
    public TMP_Text reloadText;

    void Start()
    {
        currentBullets = maxBullets;
        reloadText.gameObject.SetActive(false);
        UpdateBulletImages();
    }

    void Update()
    {
        // Detener la lógica del arma si la conversación está activa
        if (ConversationManager.Instance.IsConversationActive) return;

        // Si el cursor está desbloqueado (por ejemplo, por la interfaz), no proceses disparos
        if (Cursor.lockState == CursorLockMode.None) return;

        // Si el cursor está sobre la UI, no proceses disparos
        if (EventSystem.current.IsPointerOverGameObject()) return;

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
                ShowReloadText();
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
        canShoot = false;

        // Raycast desde el centro de la cámara
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
        UpdateBulletImages();
        audioSource.PlayOneShot(shootSound);
        StartCoroutine(ShortReload());
        StartCoroutine(ShootingCooldown());

        if (currentBullets <= 0)
        {
            ShowReloadText();
        }
    }

    private void InstantiateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, shootSpawn.position, Quaternion.LookRotation(direction));
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.velocity = direction * 100f;
        }
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
        HideReloadText();
        audioSource.PlayOneShot(fullReloadSound);
        Debug.Log("Recargando el arma");
        yield return new WaitForSeconds(fullReloadTime);
        currentBullets = maxBullets;
        UpdateBulletImages();
        isReloading = false;
        Debug.Log("Arma recargada");
    }

    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private void UpdateBulletImages()
    {
        for (int i = 0; i < bulletImages.Length; i++)
        {
            bulletImages[i].enabled = i < currentBullets;
        }
    }

    private void ShowReloadText()
    {
        reloadText.gameObject.SetActive(true);
        reloadText.text = "Presiona 'R' para recargar";
    }

    private void HideReloadText()
    {
        reloadText.gameObject.SetActive(false);
    }
}
