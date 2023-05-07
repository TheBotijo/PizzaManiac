using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class QuestPoint : MonoBehaviour
{
    public Image rawImage;
    public TextMeshProUGUI meter;
    public Vector3 offset;
    public Vector3 position;

    public void OnStart(Vector3 deliverPos)
    {
        Debug.Log(deliverPos);
        position = deliverPos;
        

    }
    // Update is called once per frame
    public void Update()
    {
        float minX = rawImage.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = rawImage.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.width - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(position + offset);

        if (Vector3.Dot((position - transform.position), transform.forward) < 0)
        {
            //Target is behind the player
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxX);

        rawImage.transform.position = pos;
        meter.text = ((int)Vector3.Distance(position, transform.position)).ToString() + "m";
    }
}
