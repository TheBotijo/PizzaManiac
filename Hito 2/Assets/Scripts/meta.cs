using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class meta : MonoBehaviour
{
    public GameObject canvas1;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("¡HAS GANADO!");
            canvas1.SetActive(true);            
        }
    }

}
