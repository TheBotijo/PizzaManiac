using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy1;
    [SerializeField]
    private GameObject enemy2;
    [SerializeField] 
    private GameObject player;

    [SerializeField] 
    private float enemy1Interval = 1f;
    [SerializeField]
    private float enemy2Interval = 1f;
    private int enemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(enemy1Interval, enemy1));
        StartCoroutine(spawnEnemy(enemy2Interval, enemy2));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {

        if (enemies < 15)
        {
            enemies++;
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f)), Quaternion.identity);
            StartCoroutine(spawnEnemy(interval, enemy));
        }


    }
}
