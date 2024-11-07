using UnityEngine;

public class BossAI : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animator; // Animator principal del Boss
    public Animator animatorAlas; // Animator de las alas
    public Quaternion angulo;
    public float grado;

    public GameObject target;
    public bool atacando;

    public float alturaVuelo = 5f; // Altura a la que se mantiene volando
    public float alturaVueloObjetivo; // Altura objetivo de vuelo que varía
    public float velocidadVuelo = 2f; // Velocidad de movimiento horizontal en el aire
    public float velocidadCambioAltura = 1f; // Velocidad para cambiar de altura
    public float radioOscilacion = 5f; // Radio de la oscilación circular alrededor del jugador
    public float frecuenciaOrbital = 0.5f; // Frecuencia de la oscilación alrededor del jugador
    public GameObject fuegoAtaque; // GameObject de fuego dentro del prefab del Boss

    public float cooldownAtaque = 3f; // Tiempo de cooldown entre ataques de fuego
    private float tiempoUltimoAtaque = 0f; // Tiempo registrado del último ataque

    private float cronometroCambioAltura; // Cronómetro para cambiar la altura objetivo
    private float tiempoParaCambioAltura = 5f; // Intervalo de tiempo para cambiar de altura objetivo
    private float tiempoOscilacion; // Tiempo acumulado para la oscilación
    private bool enPausa = false;
    private float tiempoPausa = 0f;
    public float intervaloPausa = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("FirstPersonController");
        alturaVueloObjetivo = Random.Range(0f, 18f);

        // Asegura que el fuego esté desactivado al iniciar
        if (fuegoAtaque != null)
        {
            fuegoAtaque.SetActive(false);
        }

        if (animatorAlas == null)
        {
            Debug.LogError("Animator de alas no asignado.");
        }
    }

    void Update()
    {
        Compartamiento_Enemigo();
    }

    public void Compartamiento_Enemigo()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, target.transform.position);

        if (distanciaAlJugador > 15)
        {
            animator.SetBool("Attack", false);
            cronometro += Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animator.SetBool("Attack", false);
                    SetAnimatorAlasMode(0); // Idle
                    Debug.Log("Enemigo en aire: Quieto.");
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * velocidadVuelo / 2 * Time.deltaTime);
                    SetAnimatorAlasMode(1); // Walk
                    Debug.Log("Enemigo en aire: Volando en dirección aleatoria.");
                    break;
            }
        }
        else
        {
            if (distanciaAlJugador > 6 && !atacando)
            {
                tiempoOscilacion += Time.deltaTime;
                Vector3 direccionHaciaJugador = (target.transform.position - transform.position).normalized;
                Vector3 direccionOscilacion = Vector3.Cross(direccionHaciaJugador, Vector3.up).normalized;

                Vector3 posicionConOscilacion = transform.position +
                    (direccionHaciaJugador + Mathf.Sin(tiempoOscilacion * frecuenciaOrbital) * radioOscilacion * direccionOscilacion)
                    * velocidadVuelo * Time.deltaTime;

                transform.position = posicionConOscilacion;

                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

                SetAnimatorAlasMode(1); // Walk
                animator.SetBool("Attack", false);
                Debug.Log("Enemigo en aire: Persiguiendo al jugador.");
            }
            else
            {
                if (!enPausa)
                {
                    tiempoOscilacion += Time.deltaTime * frecuenciaOrbital;
                    float angle = tiempoOscilacion;

                    Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radioOscilacion;
                    Vector3 posicionOrbital = target.transform.position + offset;

                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(posicionOrbital.x, transform.position.y, posicionOrbital.z), velocidadVuelo * Time.deltaTime);

                    var lookPos = target.transform.position - transform.position;
                    lookPos.y = 0;
                    transform.rotation = Quaternion.LookRotation(lookPos);

                    tiempoPausa += Time.deltaTime;
                    if (tiempoPausa >= intervaloPausa)
                    {
                        enPausa = true;
                        tiempoPausa = 0;
                    }

                    if (Time.time >= tiempoUltimoAtaque + cooldownAtaque)
                    {
                        tiempoUltimoAtaque = Time.time;
                    }

                    SetAnimatorAlasMode(2); // Fly
                    animator.SetBool("Attack", true);
                    atacando = true;
                    Debug.Log("Enemigo en aire: Atacando al jugador en movimiento orbital.");
                }
                else
                {
                    tiempoPausa += Time.deltaTime;
                    if (tiempoPausa >= intervaloPausa)
                    {
                        enPausa = false;
                        tiempoPausa = 0;
                    }
                }
            }
        }
    }

    public void ActivarFuego()
    {
        if (fuegoAtaque != null)
        {
            fuegoAtaque.SetActive(true);
        }
    }

    public void DesactivarFuego()
    {
        if (fuegoAtaque != null)
        {
            fuegoAtaque.SetActive(false);
        }
    }

    private void SetAnimatorAlasMode(int mode)
    {
        if (animatorAlas != null)
        {
            animatorAlas.SetInteger("Mode", mode);
        }
    }
}
