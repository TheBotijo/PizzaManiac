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
    private float enemy1Interval = 3.5f;
    [SerializeField]
    private float enemy2Interval = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(enemy1Interval, enemy1));
        StartCoroutine(spawnEnemy(enemy2Interval, enemy2));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy= Instantiate(enemy, new Vector3(Random.Range(-5f,5f), 0.5f, Random.Range(-6f, 6f)), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
