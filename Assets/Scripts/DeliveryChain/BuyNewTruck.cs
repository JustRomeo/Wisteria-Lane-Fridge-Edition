using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyNewTruck : MonoBehaviour
{
    private int nbTruck;
    public int costOfUpgrade;

    public GameObject money;
    public GameObject delivery;

    public Text costOfUpgradeText;
    // Start is called before the first frame update
    void Start()
    {
        nbTruck = 1;
        costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
    }

    public void buyTruck()
    {
        int maxNbOfTruck = delivery.GetComponent<DeliveryChain>().getMaxNbOfTruck();
        if (money.GetComponent<MoneyMaking>().getMoney() > costOfUpgrade && nbTruck < maxNbOfTruck) {
            money.GetComponent<MoneyMaking>().pay(costOfUpgrade);
            delivery.GetComponent<DeliveryChain>().addTruck();
            nbTruck += 1;
            costOfUpgrade += 2000;
            costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
        }
    }
}
