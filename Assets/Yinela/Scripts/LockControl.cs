using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    private int[] result, correctCombination;
    public Boolean Rotacion = false;

    // Start is called before the first frame update
    void Start()
    {
        result = new int[]{5,5,5};
        correctCombination = new int[] {6,4,0};
        RotatePadlock.Rotated += CheckResults;
    }

    void Update()
    {
        if(Rotacion)
        {
           Destroy(gameObject);
        }
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "wheel1":
                result[0] = number;
                Debug.Log($"wheel1: result[0] = {result[0]}, number = {number}");
                break;

            case "wheel2":
                result[1] = number;
                Debug.Log($"wheel2: result[1] = {result[1]}, number = {number}");
                break;

            case "wheel3":
                result[2] = number;
                Debug.Log($"wheel3: result[2] = {result[2]}, number = {number}");
                break;


            
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2] )
        {
            Debug.Log("Opened!");
            Rotacion = true;
        }    
    }

    private void OnDestroy()
    {
        RotatePadlock.Rotated -= CheckResults;
    }

}
