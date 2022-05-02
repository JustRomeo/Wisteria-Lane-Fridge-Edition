using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class Movement : MonoBehaviour {

    public List<Transform> destinations;

    private int row = 0;
    private NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        agent.SetDestination(destinations[row].position);
        if (Vector3.Distance(transform.position, destinations[row].position) < 20)
            row = row + 1 >= destinations.Count ? 0 : row + 1;
    }
}
