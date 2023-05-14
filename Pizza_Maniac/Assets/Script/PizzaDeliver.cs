using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PizzaDeliver : MonoBehaviour
{
    public float pizzasEntregadas = 0;
    public float pizzasTotal = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pizzasEntregadas++;
            pizzasTotal--;
            Debug.Log("Pizza entregada");
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Numero de pizzas entregadas: " );
        }
    }
}
