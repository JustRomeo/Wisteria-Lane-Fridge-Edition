using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryChain : MonoBehaviour
{
    public int nbCar;
    public int nbTruck;
    private int deliveryCapacity;

    private int maxNbOfCar;
    private int maxNbOfTruck;

    private int transportCapacityOfCars;
    private int transportCapacityOfTruck;

    public Text nbCarText;
    public Text nbTruckText;
    public Text deliveryCapacityText;

    public GameObject money;
    // Start is called before the first frame update
    void Start()
    {
        nbCar = 2;
        nbTruck = 1;

        maxNbOfCar = 5;
        maxNbOfTruck = 3;

        transportCapacityOfCars = 1;
        transportCapacityOfTruck = 3;

        deliveryCapacity = nbCar * transportCapacityOfCars + nbTruck * transportCapacityOfTruck;

        nbCarText.text = nbCar.ToString() + '/' + maxNbOfCar.ToString();
        nbTruckText.text = nbTruck.ToString() + '/' + maxNbOfTruck.ToString();
        deliveryCapacityText.text = deliveryCapacity.ToString();
    }

    public void calculateDeliveryCapacity()
    {
        deliveryCapacity = nbCar * transportCapacityOfCars + nbTruck * transportCapacityOfTruck;
        deliveryCapacityText.text = deliveryCapacity.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int getDeliveryCapacity()
    {
        return deliveryCapacity;
    }

    public void recieveDelivery(List<int> fridges)
    {
        int totalMoney = 0;
        for (int i = 0; i < fridges.Count; i++) {
            totalMoney += fridges[i];
        }
        money.GetComponent<MoneyMaking>().earnMoney(totalMoney);
    }

    public void addCar()
    {
        nbCar += 1;
        nbCarText.text = nbCar.ToString() + '/' + maxNbOfCar.ToString();
        calculateDeliveryCapacity();
    }
    public void addTruck()
    {
        nbTruck += 1;
        nbTruckText.text = nbTruck.ToString() + '/' + maxNbOfTruck.ToString();
        calculateDeliveryCapacity();
    }

    public int getMaxNbOfCar()
    {
        return (maxNbOfCar);
    }

    public int getMaxNbOfTruck()
    {
        return (maxNbOfTruck);
    }
}
