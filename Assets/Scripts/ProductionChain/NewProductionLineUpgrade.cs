using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewProductionLineUpgrade : MonoBehaviour
{
    public int stateOfUpgrade;
    public int maxUpgradeOfPL;
    public int costOfUpgrade;
    public GameObject money;

    public Text stateOfUpgradeText;
    public Text maxUpgradeOfPLText;
    public Text costOfUpgradeText;
    // Start is called before the first frame update
    void Start()
    {
        stateOfUpgradeText.text = "Actual number of production line = " + stateOfUpgrade.ToString();
        maxUpgradeOfPLText.text = "Max number of upgrade = " + maxUpgradeOfPL.ToString();
        costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
    }

    public void upgradePL()
    {

        if (money.GetComponent<MoneyMaking>().getMoney() > costOfUpgrade && stateOfUpgrade < maxUpgradeOfPL) {
            stateOfUpgrade += 1;
            costOfUpgrade *= 2;
            stateOfUpgradeText.text = "Actual number of production line = " + stateOfUpgrade.ToString();
            costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
            money.GetComponent<MoneyMaking>().pay(costOfUpgrade);
        }
    }
}
