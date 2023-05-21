using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Damage : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public bool invencible = false;
    public float time_invencible = 1f;
    //public float time_Stop = 0.2f;

    [SerializeField] 
    public HealthBar healthBar;

    public void Start()
    {
        health = maxHealth;
        healthBar.InitiateHealthBar(health);
    }

    public void loseHealth(int damage)
    {
        if(!invencible && health > 0)
        {
            health -= damage;
            healthBar.ChangeActualHealth(health);
            StartCoroutine(Invulnerability());
            //StartCoroutine(StopVelocity());
            if(health <= 0) 
            {
                Destroy(gameObject);
            }
        }
       
    }

    IEnumerator Invulnerability()
    {
        invencible = true;
        yield return new WaitForSeconds(time_invencible);
        invencible = false;
    }

    /*IEnumerator StopVelocity()
    {
        var actualVelocity = GetComponent<PlayerMoveJump>().moveSpeed;
        GetComponent<PlayerMoveJump>().moveSpeed = 0;
        yield return new WaitForSeconds(time_Stop);
        GetComponent<PlayerMoveJump>().moveSpeed = actualVelocity;
    }*/
}
