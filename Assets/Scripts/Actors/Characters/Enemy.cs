using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject agent2;

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(agent2.transform.position);
    }
}
