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

    private int maxCapacityOfCar;
    private int maxCapacityOfTruck;

    private int maintenancePercent;

    public Text nbCarText;
    public Text nbTruckText;
    public Text deliveryCapacityText;

    public GameObject money;

    private bool isCarDamaged;
    private float damageMoneyDrop;
    private float damageTimeDuration;
    private int damageType;

    private float vehicleDamageTime;

    // Start is called before the first frame update
    void Start()
    {
        nbCar = 2;
        nbTruck = 1;

        maxNbOfCar = 5;
        maxNbOfTruck = 3;

        transportCapacityOfCars = 1;
        transportCapacityOfTruck = 3;

        maxCapacityOfCar = 3;
        maxCapacityOfTruck = 7;

        maintenancePercent = 3;

        vehicleDamageTime = 0;
        damageMoneyDrop = 1;
        damageTimeDuration = 0;
        isCarDamaged = false;

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

    private void getRandomDamageType()
    {
        // int damageType = Random.Range(1, 10);

        // if (damageType == 1) {
        //     damageMoneyDrop = 0.33F;
        //     damageTimeDuration = 90;
        //     damageType = 3;
        //     return;
        // }

        // damageType = Random.Range(1, 10);
        // if (damageType <= 5) {
        //     damageMoneyDrop = 0.50F;
        //     damageTimeDuration = 60;
        //     damageType = 2;
        //     return;
        // }
        damageMoneyDrop = 0.66F;
        damageTimeDuration = 10;
        damageType = 1;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        vehicleDamageTime += Time.deltaTime;

        if (vehicleDamageTime > 20) {
            int randomDamage = Random.Range(1, 10);
            if (randomDamage > maintenancePercent && isCarDamaged == false) {
                isCarDamaged = true;
                getRandomDamageType();
                money.GetComponent<MoneyMaking>().createDamageToaster(damageType, damageMoneyDrop, damageTimeDuration, gameObject);
                StartCoroutine(coroutineStopDamage());
            }
            vehicleDamageTime = 0;
        }
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

    public void stopDamage()
    {
        vehicleDamageTime = 0;
        isCarDamaged = false;
        StopCoroutine(coroutineStopDamage());
    }

    IEnumerator coroutineStopDamage()
    {
        yield return new WaitForSeconds(damageTimeDuration);
        vehicleDamageTime = 0;
        isCarDamaged = false;
    }

    public void setMaintenancePercent(int percent)
    {
        maintenancePercent = percent;
    }

    public int getNbOfCar()
    {
        return (nbCar);
    }

    public int getNbOfTruck()
    {
        return (nbTruck);
    }

    public int getMaxNbOfCar()
    {
        return (maxNbOfCar);
    }

    public int getMaxNbOfTruck()
    {
        return (maxNbOfTruck);
    }

    public int getMaxCapacityOfCar()
    {
        return (maxCapacityOfCar);
    }

    public int getMaxCapacityOfTruck()
    {
        return (maxCapacityOfTruck);
    }

    public void increaseCarCapacity()
    {
        transportCapacityOfCars += 1;
        calculateDeliveryCapacity();
    }

    public void increaseTruckCapacity()
    {
        transportCapacityOfTruck += 2;
        calculateDeliveryCapacity();
    }
}
