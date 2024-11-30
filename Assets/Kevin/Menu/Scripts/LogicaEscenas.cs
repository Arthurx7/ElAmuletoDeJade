using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEscenas : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        var noDestruirEntreEscenas = FindObjectsOfType<LogicaEscenas>();
        if (noDestruirEntreEscenas.Length > 1) 
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
