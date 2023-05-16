using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public HealthSystem healthSystem;
    public GameObject healthBar;
    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
    }
    private void Update()
    {
        healthBar.transform.localScale= new Vector3(healthSystem.GetHealthPercent(), 1);
       // transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
}

