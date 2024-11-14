using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion angulo;
    public float grado;

    public GameObject target;
    public bool atacando;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("FirstPersonController");
    }

    void Update()
    {
        Compartamiento_Enemigo();
    }

    public void Compartamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            animator.SetBool("Run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animator.SetBool("Walk", false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 / 2 * Time.deltaTime);
                    animator.SetBool("Walk", true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1.3 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                animator.SetBool("Attack", false);
            }
            else if ((Vector3.Distance(transform.position, target.transform.position) <= 1.3))
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                animator.SetBool("Attack", true);
                atacando = true;
            }

        }

    }

    public void Final_Animacion()
    {
        animator.SetBool("Attack", false);
        atacando = false;
    }
}