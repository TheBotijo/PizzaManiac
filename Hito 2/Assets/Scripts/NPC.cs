using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public GameObject path;
    public Transform[] pathPoints;

    public int index = 0;

    public float minDistant;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();    

        pathPoints = new Transform[path.transform.childCount];
        for(int i=0; i <pathPoints.Length; i++)
        {
            pathPoints[i] = path.transform.GetChild(i);
        }
    }

    void Update()
    {

        IA();
        
    }
    void IA ()
    {
        if (Vector3.Distance(transform.position, pathPoints[index].position) < minDistant)
        {
            Debug.Log("a");
            if (index + 1 != pathPoints.Length)
            {            
                index += 1;
            }else{
                index = 0;
            }
        }

        agent.SetDestination(pathPoints[index].position);
        animator.SetFloat("vertical", !agent.isStopped ? 1 : 0);
    }
}


