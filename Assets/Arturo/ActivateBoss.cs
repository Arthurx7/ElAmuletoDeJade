using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    public GameObject boss; 
    private BossAI bossAI; 

    void Start()
    {
        bossAI = boss.GetComponent<BossAI>();
        bossAI.enabled = false; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossAI.enabled = true; 
        }
    }
}