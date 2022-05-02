using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Truck_Spawner : MonoBehaviour {
    public int[] Team;
    public Material[] teams;
    public GameObject prefab;

    private DeliveryChain _delivery;

    void Start() {
        _delivery = GameObject.FindGameObjectWithTag("Delivery").GetComponent<DeliveryChain>();
    }
    void Update() {
        for (int i = 0; i < teams.Length; i ++) {
            GameObject[] trucklist = GameObject.FindGameObjectsWithTag("Red Truck");
            // GameObject[] trucklist = GameObject.FindGameObjectsWithTag(teams[i].name + " Truck");

            // if (trucklist.Length < Team[i])
            //     create_truck(i, teams[i].name + " Truck");
            // else if (trucklist.Length > Team[i])
            //     Destroy(trucklist[0]);

            if (trucklist.Length < _delivery.nbTruck)
                create_truck(i, "Red Truck");
            else if (trucklist.Length > _delivery.nbTruck)
                Destroy(trucklist[0]);
        }
    }

    void create_truck(int team_row, string tag) {
        Vector3 _positions = transform.position;
        GameObject truck = Instantiate(prefab, _positions, Quaternion.identity);
        Movement _move = truck.GetComponent<Movement>();
        GameObject[] chckpnts = GameObject.FindGameObjectsWithTag("Checkpoint");

        truck.tag = tag;
        truck.name = tag;
        truck.GetComponent<Renderer>().material = teams[team_row];
        truck.transform.position = new Vector3(truck.transform.position.x, 0, truck.transform.position.z);

        for (int i = 0; i < 5; i ++)
            _move.destinations.Add(chckpnts[Random.Range(0, chckpnts.Length)].transform);
    }
}
