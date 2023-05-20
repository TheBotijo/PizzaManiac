using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Damage : MonoBehaviour
{
    public int health = 100;
    public bool invencible = false;
    public float time_invencible = 1f;
    public float time_Stop = 0.2f;

    public void loseHealth(int damage)
    {
        if(!invencible && health > 0)
        {
            health -= damage;
            StartCoroutine(Invulnerability());
            StartCoroutine(StopVelocity());
        }
       
    }

    IEnumerator Invulnerability()
    {
        invencible = true;
        yield return new WaitForSeconds(time_invencible);
        invencible = false;
    }

    IEnumerator StopVelocity()
    {
        var actualVelocity = GetComponent<PlayerMoveJump>().moveSpeed;
        GetComponent<PlayerMoveJump>().moveSpeed = 0;
        yield return new WaitForSeconds(time_Stop);
        GetComponent<PlayerMoveJump>().moveSpeed = actualVelocity;
    }
}
