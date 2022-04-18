using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighCostUpgrade : MonoBehaviour
{
    public int stateOfUpgrade;
    public int maxUpgradeOfPL;
    public int costOfUpgrade;
    public GameObject money;
    public GameObject productionLine;

    public Text stateOfUpgradeText;
    public Text maxUpgradeOfPLText;
    public Text costOfUpgradeText;
    // Start is called before the first frame update
    void Start()
    {
        stateOfUpgradeText.text = "Ability to manufacture high cost products = " + stateOfUpgrade.ToString();
        maxUpgradeOfPLText.text = "Max number of upgrade = " + maxUpgradeOfPL.ToString();
        costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void upgrade()
    {
        Debug.Log(money.GetComponent<MoneyMaking>().getMoney());
        if (money.GetComponent<MoneyMaking>().getMoney() > costOfUpgrade && stateOfUpgrade < maxUpgradeOfPL) {
            money.GetComponent<MoneyMaking>().pay(costOfUpgrade);
            productionLine.GetComponent<ProductionManagement>().allowHighCost();
            stateOfUpgrade += 1;
            costOfUpgrade *= 2;
            stateOfUpgradeText.text = "Ability to manufacture low cost products = " + stateOfUpgrade.ToString();
            costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
        }
    }
}
