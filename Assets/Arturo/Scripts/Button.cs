using UnityEngine;

public class Button : MonoBehaviour
{
    public Bombillo bombilloCercano;  
    private bool isPositive = false;  

    private void OnMouseDown()
    {
       
        isPositive = !isPositive;

       
        bombilloCercano.RecibirCarga(isPositive);

      
    }
}
