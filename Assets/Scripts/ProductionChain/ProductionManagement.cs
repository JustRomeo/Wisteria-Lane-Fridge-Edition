using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Position of management Page : 0, 0, 0
public class ProductionManagement : MonoBehaviour
{
    public Slider productionManagement;
    public Text fridgePriceText;
    public Text fridgeProductionPerHourText;

    private List<int> fridgeProductionList;
    private List<int> fridgePriceList;

    private int fridgePrice = 175;
    private int fridgeProductionPerHour;
    private float sliderValue;
    private int deliveryCapacity;
    private int totalFridgeToSend;

    private int opponentFridgePrice;
    private int opponentFridgeProduction;

    private int nextFridgePrice;
    private int nextFridgePerHour;
    public Text nextFridgePriceText;
    public Text nextFridgePerHourText;

    public bool lowCostFridge = false;
    public bool highCostFridge = false;

    private int lowCostValue;
    private int highCostValue;

    public GameObject money;
    public GameObject deliveryChain;
    public GameObject warehouse;

    private float moneyMakingTime;

    private float adTimeBoost;
    private float speedTimeBoost;
    private bool isAdCampainActive;
    public GameObject adPrefab;

    private bool isProductionChainDamaged;
    private float damageMoneyDrop;
    private float damageTimeDuration;
    private int damageRepairCost;

    private int maintenancePercent;
    private float productionChainDamageTime;

    private float adTime;

    private float time;

    public GameObject Canvas;
    public GameObject ToasterList;

    void Start()
    {
        moneyMakingTime = 5;
        nextFridgePerHour = fridgeProductionPerHour;
        productionManagement.onValueChanged.AddListener(delegate {productionManagementValueChange ();});
        sliderValue = productionManagement.value;
        fridgePrice = 175;
        fridgeProductionPerHour = 7;
        nextFridgePrice = fridgePrice;
        nextFridgePerHour = fridgeProductionPerHour;
        fridgeProductionPerHourText.text  = fridgeProductionPerHour.ToString() + "/h";
        adTime = 0;
        maintenancePercent = 5;

        lowCostValue = 3;
        highCostValue = 12;

        fridgeProductionList =  new List<int>{25, 17, 13, 10, 7, 5, 3, 2, 1};
        fridgePriceList = new List<int>{80, 105, 120, 140, 175, 280, 535, 900, 2000};
        calculateMoneyEarning();
        productionManagementValueChange();
    }

    public bool isProductionDamaged()
    {
        return (isProductionChainDamaged);
    }

    private VehicleDamageStat productionChainDamage()
    {
        int damageType = Random.Range(1, 10);
        VehicleDamageStat damageStat = new VehicleDamageStat();

        if (damageType == 1) {
            damageMoneyDrop = 0.33F;
            damageTimeDuration = 90;
            damageType = 3;
            damageRepairCost = 5000;
            damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
            return (damageStat);
        }
        else {
            damageType = Random.Range(1, 10);
            if (damageType <= 6) {
                damageMoneyDrop = 0.50F;
                damageTimeDuration = 60;
                damageType = 2;
                damageRepairCost = 3000;
                damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
                return (damageStat);
            }
            else {
                damageMoneyDrop = 0.66F;
                damageTimeDuration = 30;
                damageType = 1;
                damageRepairCost = 1500;
                damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
                return (damageStat);
            }
        }
    }

    public void productionSabotage(int damageType)
    {
        VehicleDamageStat damageStat = new VehicleDamageStat();

        if (damageType == 3) {
            damageMoneyDrop = 0.33F;
            damageTimeDuration = 30;
            damageType = 3;
            damageRepairCost = 1500;
            damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
        }
        if (damageType == 2) {
            damageMoneyDrop = 0.50F;
            damageTimeDuration = 60;
            damageType = 2;
            damageRepairCost = 3000;
            damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
        }
        if (damageType == 1) {
            damageMoneyDrop = 0.66F;
            damageTimeDuration = 90;
            damageType = 1;
            damageRepairCost = 5000;
            damageStat.setData(damageMoneyDrop, damageTimeDuration, damageType, damageRepairCost);
        }
        isProductionChainDamaged = true;
        money.GetComponent<MoneyMaking>().createProductionDamageToaster(damageStat, gameObject);
        StartCoroutine(coroutineStopDamage());
    }


    // Update is called once per frame
    void Update()
    {
        calculateMoneyEarning();
        float deltaTime = Time.deltaTime;
        time += deltaTime;
        productionChainDamageTime += deltaTime;
        if (isAdCampainActive) {
            adTime += deltaTime;
            if (adTime > adTimeBoost) {
                adTimeBoost = 0;
                adTime = 0;
                isAdCampainActive = false;
                moneyMakingTime = 5;
            }
        }
        if (time > moneyMakingTime) {
            time = 0;
            deliveryCapacity = deliveryChain.GetComponent<DeliveryChain>().getDeliveryCapacity();
            List<int> fridgeToSend = createListOfFridgeToSend();
            if (deliveryCapacity < fridgeProductionPerHour) {
                warehouse.GetComponent<WarehouseNB>().addFridge(fridgeProductionPerHour - totalFridgeToSend, fridgePrice);
            }
            else {
                List<int> fridgeInWareHouse = warehouse.GetComponent<WarehouseNB>().removeFridge(deliveryCapacity - totalFridgeToSend);
                fridgeToSend.AddRange(fridgeInWareHouse);
            }
            deliveryChain.GetComponent<DeliveryChain>().recieveDelivery(fridgeToSend);
            updateFridgeProduction();
        }
        if (productionChainDamageTime > 10)
        {
            int randomDamage = Random.Range(1, 10);
            if (randomDamage > maintenancePercent && isProductionChainDamaged == false) {
                isProductionChainDamaged = true;
                VehicleDamageStat damageStat = productionChainDamage();
                money.GetComponent<MoneyMaking>().createProductionDamageToaster(damageStat, gameObject);
                StartCoroutine(coroutineStopDamage());
            }
            productionChainDamageTime = 0;
        }
    }

    void updateFridgeProduction()
    {
        fridgePrice = nextFridgePrice;
        fridgeProductionPerHour = nextFridgePerHour;

        fridgePriceText.text = fridgePrice.ToString() + "$";
        fridgeProductionPerHourText.text  = fridgeProductionPerHour.ToString() + "/h";
        calculateMoneyEarning();
    }

    public void productionManagementValueChange()
    {
        int price = 0;
        int production = 0;
        if (productionManagement.value < lowCostValue && lowCostFridge == false) {
            productionManagement.value = 3;
            return;
        }
        if (productionManagement.value > highCostValue && highCostFridge == false) {
            productionManagement.value = 5;
            return;
        }
        sliderValue = productionManagement.value;
        price = fridgePriceList[(int)productionManagement.value];
        production = fridgeProductionList[(int)productionManagement.value];

        nextFridgePerHour = production;
        nextFridgePrice = price;

        nextFridgePriceText.text = nextFridgePrice.ToString() + "$";
        nextFridgePerHourText.text = nextFridgePerHour.ToString() + "/h";
    }

    void calculateMoneyEarning()
    {
        deliveryCapacity = deliveryChain.GetComponent<DeliveryChain>().getDeliveryCapacity();
        if (fridgeProductionPerHour > deliveryCapacity) {
            money.GetComponent<MoneyMaking>().increaseMoneyEarning(fridgePrice * deliveryCapacity);
            totalFridgeToSend = deliveryCapacity;
        }
        else {
            List<int> fridgeInWarehouse = warehouse.GetComponent<WarehouseNB>().getNextExtract(deliveryCapacity - totalFridgeToSend);
            int moneySupplement = 0;
            for (int i = 0; i < fridgeInWarehouse.Count; i++)
                moneySupplement += fridgeInWarehouse[i];
            money.GetComponent<MoneyMaking>().increaseMoneyEarning(fridgePrice * fridgeProductionPerHour + moneySupplement);
            totalFridgeToSend = fridgeProductionPerHour;
        }

    }

    private List<int> createListOfFridgeToSend()
    {
        List<int> fridgeToSend = new List<int>();
        for (int i = 0; i < totalFridgeToSend; i++)
            fridgeToSend.Add(fridgePrice);
        return (fridgeToSend);
    }

    public void allowLowCost()
    {
        lowCostFridge = true;
    }

    public void allowHighCost()
    {
        highCostFridge = true;
    }

    public void createAdd(float timeBoost, float speedBoost, string adName)
    {
        adTime = 0;
        isAdCampainActive = true;
        adTimeBoost = timeBoost;
        moneyMakingTime = moneyMakingTime * speedBoost;
        createAdInfoCanvas(adName);
    }

    void createAdInfoCanvas(string adName)
    {
        string adTitle = "campain \"" + adName + "\" launched\nBoosting speed";

        GameObject newAd = Instantiate(adPrefab, new Vector3(610, 310, 0), Quaternion.identity);

        newAd.transform.SetParent(Canvas.transform, false);
        newAd.GetComponent<AdToaster>().setRemainingTime(adTimeBoost);
        newAd.GetComponent<AdToaster>().setAdTitle(adTitle);
        newAd.GetComponent<AdToaster>().setToasterList(ToasterList);

        ToasterList.GetComponent<ToasterList>().addToaster(newAd);
    }

    public void stopDamage()
    {
        productionChainDamageTime = 0;
        isProductionChainDamaged = false;
        StopCoroutine(coroutineStopDamage());
    }

    IEnumerator coroutineStopDamage()
    {
        yield return new WaitForSeconds(damageTimeDuration);
        productionChainDamageTime = 0;
        damageMoneyDrop = 0;
        damageRepairCost = 0;
        isProductionChainDamaged = false;
    }

    public int getFridgeProduction()
    {
        return (fridgeProductionPerHour);
    }
    public int getFridgePrice()
    {
        return (fridgePrice);
    }

    public void setOpponentFridgePrice(int price)
    {
        opponentFridgePrice = price;
    }

    public void setOpponentFridgeProduction(int production)
    {
        opponentFridgeProduction = production;
    }

    public void setProdMaintenanceBudget(int percent)
    {
        maintenancePercent = percent;
    }
    public int getMaintenancePercent()
    {
        return (maintenancePercent);
    }

    public void increaseMaxProduction()
    {
        fridgeProductionList[0] += 4;
        fridgeProductionList[1] += 3;
        fridgeProductionList[2] += 3;
        fridgeProductionList[3] += 2;
        fridgeProductionList[4] += 2;
        fridgeProductionList[5] += 1;
        fridgeProductionList[6] += 1;

        productionManagementValueChange();
        updateFridgeProduction();
    }

    public void increaseMaxPrice()
    {
        fridgePriceList[1] += 5;
        fridgePriceList[2] += 10;
        fridgePriceList[3] += 13;
        fridgePriceList[4] += 25;
        fridgePriceList[5] += 45;
        fridgePriceList[6] += 85;
        fridgePriceList[7] += 175;
        fridgePriceList[8] += 350;

        productionManagementValueChange();
        updateFridgeProduction();
    }
}
