using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighCostUpgrade : MonoBehaviour
{
    public int stateOfUpgrade;
    public int maxUpgrade;
    public int costOfUpgrade;
    public GameObject money;
    public GameObject productionLine;
    public GameObject upgradeButton;

    public Text stateOfUpgradeText;
    public Text maxUpgradeText;
    public Text costOfUpgradeText;
    // Start is called before the first frame update
    void Start()
    {
        stateOfUpgradeText.text = "Ability to manufacture high cost products = " + stateOfUpgrade.ToString();
        maxUpgradeText.text = "Max number of upgrade = " + maxUpgrade.ToString();
        costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void upgrade()
    {
        Debug.Log(money.GetComponent<MoneyMaking>().getMoney());
        if (money.GetComponent<MoneyMaking>().getMoney() > costOfUpgrade && stateOfUpgrade < maxUpgrade) {
            money.GetComponent<MoneyMaking>().pay(costOfUpgrade);
            productionLine.GetComponent<ProductionManagement>().allowHighCost();
            stateOfUpgrade += 1;
            costOfUpgrade *= 2;
            stateOfUpgradeText.text = "Ability to manufacture low cost products = " + stateOfUpgrade.ToString();
            if (stateOfUpgrade == maxUpgrade) {
                costOfUpgradeText.text = "";
                upgradeButton.SetActive(false);
            }
            else
                costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
        }
    }
}
