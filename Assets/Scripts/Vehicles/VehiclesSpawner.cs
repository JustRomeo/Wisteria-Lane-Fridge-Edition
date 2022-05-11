using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehiclesSpawner : MonoBehaviour {
    public int[] Team;
    public Material[] teams;
    public GameObject car_prefab;
    public GameObject truck_prefab;

    private DeliveryChain _delivery;

    void Start() {
        _delivery = GameObject.FindGameObjectWithTag("Delivery").GetComponent<DeliveryChain>();
    }
    void Update() {
        updateTruck();
        updateCar();
    }

    void updateTruck() {
        for (int i = 0; i < teams.Length; i ++) {
            GameObject[] trucklist = GameObject.FindGameObjectsWithTag("Red Truck");

            if (trucklist.Length < _delivery.nbTruck)
                create_truck(i, "Red Truck");
            else if (trucklist.Length > _delivery.nbTruck)
                Destroy(trucklist[0]);
        }
    } void updateCar() {
        for (int i = 0; i < teams.Length; i ++) {
            GameObject[] carlist = GameObject.FindGameObjectsWithTag("Red Car");

            if (carlist.Length < _delivery.nbCar)
                create_car(i, "Red Car");
            else if (carlist.Length > _delivery.nbCar)
                Destroy(carlist[0]);
        }
    }

    void create_truck(int team_row, string tag) {
        Vector3 _positions = transform.position;
        GameObject truck = Instantiate(truck_prefab, _positions, Quaternion.identity);
        Movement _move = truck.GetComponent<Movement>();
        GameObject[] chckpnts = GameObject.FindGameObjectsWithTag("Checkpoint");

        truck.tag = tag;
        truck.name = tag;
        truck.GetComponent<Renderer>().material = teams[team_row];
        truck.transform.position = new Vector3(truck.transform.position.x, 0, truck.transform.position.z);

        _move.randomPathGeneration(9);
    } void create_car(int team_row, string tag) {
        Vector3 _positions = transform.position;
        GameObject car = Instantiate(car_prefab, _positions, Quaternion.identity);
        Movement _move = car.GetComponent<Movement>();
        GameObject[] chckpnts = GameObject.FindGameObjectsWithTag("Checkpoint");

        car.tag = tag;
        car.name = tag;
        car.GetComponent<Renderer>().material = teams[team_row];
        car.transform.position = new Vector3(car.transform.position.x, 0, car.transform.position.z);

        _move.randomPathGeneration(9);
    }
}
