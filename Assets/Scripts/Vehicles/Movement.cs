using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class Movement : MonoBehaviour {

    public List<Transform> destinations;

    private int row = 0;
    private NavMeshAgent agent;

    void Start() {
        if (destinations.Count < 2)
            randomPathGeneration(5);
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        agent.SetDestination(destinations[row].position);
        if (Vector3.Distance(transform.position, destinations[row].position) < 20)
            row = row + 1 >= destinations.Count ? 0 : row + 1;
    }

    public void randomPathGeneration(int checkNbr) {
        GameObject[] chckpnts = GameObject.FindGameObjectsWithTag("Checkpoint");

        for (int i = 0; i < checkNbr; i ++)
            destinations.Add(chckpnts[Random.Range(0, chckpnts.Length)].transform);
    }
}
