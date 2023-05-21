using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    public Enemy2 enemyDamage;

    public int damagee;

    // Start is called before the first frame update
    void Start()
    {
        damagee = enemyDamage.damage;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health_Damage>().loseHealth(damagee);
        }
    }
}
