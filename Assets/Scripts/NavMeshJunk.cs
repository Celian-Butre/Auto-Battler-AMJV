using UnityEngine;
using UnityEngine.AI;

public class NavMeshJunk : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Vector3 destination;
    [SerializeField] GameObject Arriv;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    // Update is called once per frame
    void Update()
    {
        destination=Arriv.transform.position;
        agent.destination = destination;

    }
}
