using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryChain : MonoBehaviour
{
    public bool isDeliveryActive;

    public int nbCar;
    public int nbTruck;
    private int deliveryCapacity;

    private int maxNbOfCar;
    private int maxNbOfTruck;

    private int transportCapacityOfCars;
    private int transportCapacityOfTruck;

    private int maxCapacityOfCar;
    private int maxCapacityOfTruck;

    private int opponentNbCar;
    private int opponentNbTruck;
    private int opponentCarCapacity;
    private int opponentTruckCapacity;
    private int opponentMaintenancePercent;

    private int maintenancePercent;

    public Text nbCarText;
    public Text nbTruckText;
    public Text deliveryCapacityText;

    public GameObject money;
    public GameObject unlockedCanvas;

    private bool isCarDamaged;
    private float damageMoneyDrop;
    private float damageTimeDuration;
    private int damageRepairCost;

    private float vehicleDamageTime;

    // Start is called before the first frame update
    void Start()
    {
        isDeliveryActive = false;

        nbCar = 1;
        nbTruck = 0;

        maxNbOfCar = 5;
        maxNbOfTruck = 3;

        transportCapacityOfCars = 1;
        transportCapacityOfTruck = 3;

        maxCapacityOfCar = 3;
        maxCapacityOfTruck = 7;

        maintenancePercent = 5;

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

    private VehicleDamageStat lowMaintenanceDamage()
    {
        int damageType = Random.Range(1, 10);
        VehicleDamageStat damageStat = new VehicleDamageStat();

        if (damageType <= 2) {
            damageMoneyDrop = 0.33F;
            damageTimeDuration = 90;
            damageType = 3;
            damageRepairCost = nbCar * 1750 + nbTruck * 3500;
            damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
            return (damageStat);
        }
        else {
            damageType = Random.Range(1, 10);
            if (damageType <= 6) {
                damageMoneyDrop = 0.50F;
                damageTimeDuration = 60;
                damageType = 2;
                damageRepairCost = nbCar * 1250 + nbTruck * 2250;
                damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
                return (damageStat);
            }
            else {
                damageMoneyDrop = 0.66F;
                damageTimeDuration = 30;
                damageType = 1;
                damageRepairCost = nbCar * 500 + nbTruck * 1000;
                damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
                return (damageStat);
            }
        }
    }

    private VehicleDamageStat highMaintenanceDamage()
    {
        int damageType = Random.Range(1, 10);
        VehicleDamageStat damageStat = new VehicleDamageStat();

        if (damageType <= 3) {
            damageMoneyDrop = 0.50F;
            damageTimeDuration = 60;
            damageType = 2;
            damageRepairCost = nbCar * 1250 + nbTruck * 2250;
            damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
            return (damageStat);
        }
        damageMoneyDrop = 0.66F;
        damageTimeDuration = 30;
        damageType = 1;
        damageRepairCost = nbCar * 500 + nbTruck * 1000;
        damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
        return (damageStat);
    }

    private VehicleDamageStat getVehicleDamage()
    {
        if (maintenancePercent >= 7)
            return (highMaintenanceDamage());
        else
            return (lowMaintenanceDamage());
    }

    public void vehicleSabotage(int damageType)
    {
        VehicleDamageStat damageStat = new VehicleDamageStat();

        if (damageType == 1) {
            damageMoneyDrop = 0.66F;
            damageTimeDuration = 30;
            damageType = 1;
            damageRepairCost = nbCar * 500 + nbTruck * 1000;
            damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
        }
        if (damageType == 2) {
            damageMoneyDrop = 0.50F;
            damageTimeDuration = 60;
            damageType = 2;
            damageRepairCost = nbCar * 1250 + nbTruck * 2250;
            damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
        }
        if (damageType == 3) {
            damageMoneyDrop = 0.33F;
            damageTimeDuration = 90;
            damageType = 3;
            damageRepairCost = nbCar * 1750 + nbTruck * 3500;
            damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
        }
        isCarDamaged = true;
        money.GetComponent<MoneyMaking>().createCarDamageToaster(damageStat, gameObject);
        StartCoroutine(coroutineStopDamage());
    }

    public bool isVehicleDamaged()
    {
        return (isCarDamaged);
    }

    // Update is called once per frame
    void Update()
    {
        vehicleDamageTime += Time.deltaTime;

        if (vehicleDamageTime > 30 && isDeliveryActive == true) {
            int randomDamage = Random.Range(1, 10);
            if (randomDamage > maintenancePercent && isCarDamaged == false) {
                isCarDamaged = true;
                VehicleDamageStat damageStat = getVehicleDamage();
                money.GetComponent<MoneyMaking>().createCarDamageToaster(damageStat, gameObject);
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
        damageMoneyDrop = 0;
        damageRepairCost = 0;
        isCarDamaged = false;
    }

    public void setMaintenancePercent(int percent)
    {
        maintenancePercent = percent;
    }
    public int getMaintenancePercent()
    {
        return (maintenancePercent);
    }

    //vehicle number function
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
    //transport capacity function
    public int getCarTransportCapacity()
    {
        return (transportCapacityOfCars);
    }
    public int getTruckTransportCapacity()
    {
        return (transportCapacityOfTruck);
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

    //opponent data
    public void setOpponentVehicleNb(int carNb, int truckNb)
    {
        opponentNbCar = carNb;
        opponentNbTruck = truckNb;
    }

    public void setOpponentVehicleCapacity(int carPlace, int truckPlace)
    {
        opponentCarCapacity = carPlace;
        opponentTruckCapacity = truckPlace;
    }
    public void setOpponentMaintenancePercent(int maintenance)
    {
        opponentMaintenancePercent = maintenance;
    }

    public void setActiveDelivery()
    {
        nbCar = 2;
        nbTruck = 1;
        isDeliveryActive = true;
        unlockedCanvas.SetActive(false);
        vehicleDamageTime = 0;
        nbCarText.text = nbCar.ToString() + '/' + maxNbOfCar.ToString();
        nbTruckText.text = nbTruck.ToString() + '/' + maxNbOfTruck.ToString();
        calculateDeliveryCapacity();
    }
}
