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

    private int fridgePrice;
    private int fridgeProductionPerHour;
    private float sliderValue;
    private int deliveryCapacity;
    private int totalFridgeToSend;

    private int nextFridgePrice;
    private int nextFridgePerHour;
    public Text nextFridgePriceText;
    public Text nextFridgePerHourText;


    public bool lowCostFridge = false;
    public bool highCostFridge = false;

    public GameObject money;
    public GameObject deliveryChain;
    public GameObject warehouse;

    private float moneyMakingTime;

    private float adTimeBoost;
    private float speedTimeBoost;
    private bool isAdCampainActive;
    public GameObject adPrefab;


    private float adTime;

    private float time;

    public GameObject Canvas;

    void Start()
    {
        moneyMakingTime = 5;
        nextFridgePerHour = fridgeProductionPerHour;
        // productionManagement = GetComponent<Slider>();
        productionManagement.onValueChanged.AddListener(delegate {productionManagementValueChange ();});
        sliderValue = productionManagement.value;
        fridgePrice = 250;
        fridgeProductionPerHour = 5;
        nextFridgePrice = fridgePrice;
        nextFridgePerHour = fridgeProductionPerHour;
        fridgeProductionPerHourText.text  = fridgeProductionPerHour.ToString() + "/h";
        adTime = 0;
        calculateMoneyEarning();
    }

    // Update is called once per frame
    void Update()
    {
        calculateMoneyEarning();
        float deltaTime = Time.deltaTime;
        time += deltaTime;
        adTime += deltaTime;
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
        if (productionManagement.value < 3 && lowCostFridge == false) {
            productionManagement.value = 3;
            return;
        }
        if (productionManagement.value > 12 && highCostFridge == false) {
            productionManagement.value = 12;
            return;
        }
        sliderValue = productionManagement.value;
        nextFridgePerHour = (int)((5 / sliderValue) * 5);
        nextFridgePrice = (int)sliderValue * 50;
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
    }
}
