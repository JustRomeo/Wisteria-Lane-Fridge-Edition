using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTruckCapacity : MonoBehaviour
{
    private int stateOfUpgrade;
    private int maxNBOfUpgrade;
    public int costOfUpgrade;
    public GameObject money;

    public GameObject deliveryChain;

    public Text truckCapicityText;
    public Text costOfUpgradeText;
    // Start is called before the first frame update
    void Start()
    {
        stateOfUpgrade = 3;
        maxNBOfUpgrade = deliveryChain.GetComponent<DeliveryChain>().getMaxCapacityOfTruck();

        truckCapicityText.text = "Truck transport capcity = " + stateOfUpgrade.ToString() + "/" + maxNBOfUpgrade.ToString();
        costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void increaseTruckCapacity()
    {

        if (money.GetComponent<MoneyMaking>().getMoney() > costOfUpgrade && stateOfUpgrade < maxNBOfUpgrade)
         {
            money.GetComponent<MoneyMaking>().pay(costOfUpgrade);
            stateOfUpgrade += 2;
            costOfUpgrade *= 2;
            truckCapicityText.text = "Truck transport capcity = " + stateOfUpgrade.ToString() + "/" + maxNBOfUpgrade.ToString();
            if (stateOfUpgrade == maxNBOfUpgrade)
                costOfUpgradeText.text = "";
            else
                costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
            deliveryChain.GetComponent<DeliveryChain>().increaseTruckCapacity();
        }
    }
}
