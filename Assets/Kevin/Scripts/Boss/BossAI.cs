using UnityEngine;

public class BossAI : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion angulo;
    public float grado;

    public GameObject target;
    public bool atacando;

    public float alturaVuelo = 5f; // Altura a la que se mantiene volando
    public float alturaVueloObjetivo; // Altura objetivo de vuelo que var�a
    public float velocidadVuelo = 2f; // Velocidad de movimiento horizontal en el aire
    public float velocidadCambioAltura = 1f; // Velocidad para cambiar de altura
    public float radioOscilacion = 5f; // Radio de la oscilaci�n circular alrededor del jugador
    public float frecuenciaOrbital = 0.5f; // Frecuencia de la oscilaci�n alrededor del jugador
    public GameObject fuegoAtaque; // GameObject de fuego dentro del prefab del Boss

    public float cooldownAtaque = 3f; // Tiempo de cooldown entre ataques de fuego
    private float tiempoUltimoAtaque = 0f; // Tiempo registrado del �ltimo ataque

    private float cronometroCambioAltura; // Cron�metro para cambiar la altura objetivo
    private float tiempoParaCambioAltura = 5f; // Intervalo de tiempo para cambiar de altura objetivo
    private float tiempoOscilacion; // Tiempo acumulado para la oscilaci�n
    private bool enPausa = false;
    private float tiempoPausa = 0f;
    public float intervaloPausa = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("FirstPersonController");
        alturaVueloObjetivo = Random.Range(0f, 18f);

        // Asegura que el fuego est� desactivado al iniciar
        if (fuegoAtaque != null)
        {
            fuegoAtaque.SetActive(false);
        }
    }

    void Update()
    {
        Compartamiento_Enemigo();
    }

    public void Compartamiento_Enemigo()
    {
        // Cambia el rango de altura basado en si el enemigo est� atacando o no
        float maxAltura = atacando ? 10f : 18f;

        // Controla la altura del enemigo con el rango ajustado
        cronometroCambioAltura += Time.deltaTime;
        if (cronometroCambioAltura >= tiempoParaCambioAltura)
        {
            alturaVueloObjetivo = Random.Range(0f, maxAltura); // Define el nuevo l�mite de altura
            cronometroCambioAltura = 0;
            Debug.Log($"Nueva altura de vuelo objetivo: {alturaVueloObjetivo}");
        }

        float nuevaAltura = Mathf.MoveTowards(transform.position.y, alturaVueloObjetivo, velocidadCambioAltura * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, nuevaAltura, transform.position.z);

        // Distancia del enemigo al jugador
        float distanciaAlJugador = Vector3.Distance(transform.position, target.transform.position);

        if (distanciaAlJugador > 15)
        {
            animator.SetBool("Run", false);
            cronometro += Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animator.SetBool("Walk", false);
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
                    animator.SetBool("Walk", true);
                    Debug.Log("Enemigo en aire: Volando en direcci�n aleatoria.");
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

                // Calcula la posici�n con oscilaci�n
                Vector3 posicionConOscilacion = transform.position +
                    (direccionHaciaJugador + Mathf.Sin(tiempoOscilacion * frecuenciaOrbital) * radioOscilacion * direccionOscilacion)
                    * velocidadVuelo * Time.deltaTime;

                transform.position = posicionConOscilacion;

                // Ajusta la rotaci�n para mirar al jugador
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
                Debug.Log("Enemigo en aire: Persiguiendo al jugador.");
            }
            else
            {
                // Movimiento orbital alrededor del jugador con pausa y acercamiento gradual
                if (!enPausa)
                {
                    tiempoOscilacion += Time.deltaTime * frecuenciaOrbital;
                    float angle = tiempoOscilacion;

                    // Calcula la posici�n objetivo en el c�rculo alrededor del jugador
                    Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radioOscilacion;
                    Vector3 posicionOrbital = target.transform.position + offset;

                    // Mueve al enemigo gradualmente hacia la posici�n orbital sin teletransportarse
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(posicionOrbital.x, transform.position.y, posicionOrbital.z), velocidadVuelo * Time.deltaTime);

                    // Asegura que el NPC siempre est� mirando al jugador al atacar
                    var lookPos = target.transform.position - transform.position;
                    lookPos.y = 0;
                    transform.rotation = Quaternion.LookRotation(lookPos);

                    // Controla la pausa despu�s de un intervalo
                    tiempoPausa += Time.deltaTime;
                    if (tiempoPausa >= intervaloPausa)
                    {
                        enPausa = true;
                        tiempoPausa = 0;
                    }

                    // Lanza el fuego si el cooldown ha terminado
                    if (Time.time >= tiempoUltimoAtaque + cooldownAtaque)
                    {
                        ActivarFuego();
                        tiempoUltimoAtaque = Time.time; // Actualiza el tiempo del �ltimo ataque
                    }

                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", true);
                    atacando = true;
                    Debug.Log("Enemigo en aire: Atacando al jugador en movimiento orbital.");
                }
                else
                {
                    // Pausa en la oscilaci�n
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
        // Activa el GameObject de fuego y lo orienta hacia el jugador
        if (fuegoAtaque != null)
        {
            fuegoAtaque.SetActive(true);

            // Orienta el fuego hacia el jugador
            Vector3 direccionHaciaJugador = (target.transform.position - fuegoAtaque.transform.position).normalized;
            fuegoAtaque.transform.rotation = Quaternion.LookRotation(direccionHaciaJugador);

            // Desactiva el fuego despu�s de un breve tiempo
            Invoke("DesactivarFuego", 3f); // Desactiva el fuego tras 1 segundo
        }

        Debug.Log("Lanzando fuego al jugador.");
    }

    public void DesactivarFuego()
    {
        if (fuegoAtaque != null)
        {
            fuegoAtaque.SetActive(false);
        }
    }

    public void Final_Animacion()
    {
        animator.SetBool("Attack", false);
        atacando = false;
    }
}
