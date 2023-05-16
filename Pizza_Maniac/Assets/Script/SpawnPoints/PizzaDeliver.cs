using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class PizzaDeliver : MonoBehaviour
{
    public int pizzasEntregadas = 0;
    public int currentPizzas;
    public SpawnPoints spawnPoint;
    public GameObject deliverPoint;
    //public TextMeshPro repartirText;

    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(currentPizzas == 0)
            {
                currentPizzas = 10;

            }
            else
            {
                pizzasEntregadas++;
                currentPizzas--;
                Debug.Log("Pizza entregada");
            }

            spawnPoint.pizzas = currentPizzas;
            spawnPoint.respawn(deliverPoint);
        }
    }
}
