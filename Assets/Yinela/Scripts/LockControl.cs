using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    private int[] result, correctCombination;
    
    // Start is called before the first frame update
    void Start()
    {
        result = new int[]{0,0,0,0,0};
        correctCombination = new int[] {1,2,3,4,5};
        RotatePadlock.Rotated += CheckResults;
        
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

            case "wheel4":
                result[3] = number;
                Debug.Log($"wheel4: result[3] = {result[3]}, number = {number}");
                break;

            case "wheel5":
                result[4] = number;
                Debug.Log($"wheel5: result[4] = {result[4]}, number = {number}");
                break;
        }
        
        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2] && result[3] == correctCombination[3] && result[4] == correctCombination[4])
        {
            Debug.Log("Opened!");
        }
    }

    private void OnDestroy()
    {
        RotatePadlock.Rotated -= CheckResults;
    }

}
