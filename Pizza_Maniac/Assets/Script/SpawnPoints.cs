using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] int spawnPoints = 3;
    [SerializeField] List<GameObject> spawnPointList;
    [SerializeField] List<GameObject> deliverPointList;
    public GameObject player;
    public GameObject deliverPoint;

    private void Start()
    {
        respawn(player, deliverPoint);
    }
    public void respawn(GameObject player, GameObject deliverPoint)
    {
        int spawnPoint = Random.Range(0,spawnPoints);
        player.transform.position = spawnPointList[spawnPoint].transform.position;
        player.transform.localRotation = spawnPointList[spawnPoint].transform.localRotation;
        deliverPoint.transform.position = deliverPointList[spawnPoint].transform.position;
    }
}
