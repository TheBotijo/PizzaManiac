using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Corapizzas;
    [SerializeField] private Sprite CoraDead;
    public GameObject DeadUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int index)
    {
        Image CoraPiza = Corapizzas[index].GetComponent<Image>();
        CoraPiza.sprite = CoraDead;
        if (index == 0)
        {
            DeadUI.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("OllTogeder");
            }
        }
    }
}
