using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewProductionLineUpgrade : MonoBehaviour
{
    public int stateOfUpgrade;
    public int maxUpgradeOfPL;
    public int costOfUpgrade;
    private float increaseCostMultiplier;
    public GameObject money;

    public Text stateOfUpgradeText;
    public Text maxUpgradeOfPLText;
    public Text costOfUpgradeText;

    public GameObject productionLine;
    // Start is called before the first frame update
    void Start()
    {
        stateOfUpgradeText.text = "Actual number of production line = " + stateOfUpgrade.ToString();
        maxUpgradeOfPLText.text = "Max number of upgrade = " + maxUpgradeOfPL.ToString();
        costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
        increaseCostMultiplier = 1.5f;
    }

    public void increaseMaxUpgrade()
    {
        maxUpgradeOfPL += 4;
        maxUpgradeOfPLText.text = "Max number of upgrade = " + maxUpgradeOfPL.ToString();
        // increaseCostMultiplier *= 1.65f;
    }

    public void upgradePL()
    {
        if (money.GetComponent<MoneyMaking>().getMoney() > costOfUpgrade && stateOfUpgrade < maxUpgradeOfPL) {
            money.GetComponent<MoneyMaking>().pay(costOfUpgrade);
            stateOfUpgrade += 1;
            costOfUpgrade = (int)(costOfUpgrade * increaseCostMultiplier);
            stateOfUpgradeText.text = "Actual number of production line = " + stateOfUpgrade.ToString();
            costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
            productionLine.GetComponent<ProductionManagement>().increaseMaxProduction();
        }
    }
}
