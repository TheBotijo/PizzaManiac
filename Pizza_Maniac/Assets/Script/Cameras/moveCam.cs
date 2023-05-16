using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class moveCam : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform orientation;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
