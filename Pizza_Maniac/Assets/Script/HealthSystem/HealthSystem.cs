using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour 
{
    public int health;
    public int healthMax = 100;


   public HealthSystem(int healthMax) {
        this.health = healthMax;
        health=healthMax;
   }
   public int GetHealth(){
        return health;
   }
   public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }
   public void Damage (int damageAmount) {
        Debug.Log("Entra");
        health -= damageAmount;
        if (health < 0)
        {
            health = 0;
        }
   }
   public void Heal(int healAmount){
        Debug.Log("Entra2");
        health += healAmount;
        if (health > healthMax)
        {
            health = healthMax;
        }
   }
}
