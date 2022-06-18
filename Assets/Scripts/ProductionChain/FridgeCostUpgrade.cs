using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FridgeCostUpgrade : MonoBehaviour
{
    public int stateOfUpgrade;
    public int maxUpgradeOfCost;
    public int costOfUpgrade;
    private float increaseCostMultiplier;
    public GameObject money;

    public Text stateOfUpgradeText;
    public Text maxUpgradeOfCostText;
    public Text costOfUpgradeText;

    public GameObject productionLine;
    // Start is called before the first frame update
    void Start()
    {
        stateOfUpgradeText.text = "Number of upgrade bought = " + stateOfUpgrade.ToString();
        maxUpgradeOfCostText.text = "Max number of upgrade = " + maxUpgradeOfCost.ToString();
        costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
        increaseCostMultiplier = 2;
    }

    public void increaseMaxUpgrade()
    {
        maxUpgradeOfCost += 4;
        maxUpgradeOfCostText.text = "Max number of upgrade = " + maxUpgradeOfCost.ToString();
    }

    public void upgradeCost()
    {
        if (money.GetComponent<MoneyMaking>().getMoney() > costOfUpgrade && stateOfUpgrade < maxUpgradeOfCost) {
            money.GetComponent<MoneyMaking>().pay(costOfUpgrade);
            stateOfUpgrade += 1;
            costOfUpgrade = (int)(costOfUpgrade * increaseCostMultiplier);
            stateOfUpgradeText.text = "Actual number of production line = " + stateOfUpgrade.ToString();
            costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
            productionLine.GetComponent<ProductionManagement>().increaseMaxPrice();
        }
    }
}
