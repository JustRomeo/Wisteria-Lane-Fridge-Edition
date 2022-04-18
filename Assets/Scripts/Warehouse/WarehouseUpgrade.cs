using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WarehouseUpgrade : MonoBehaviour
{
   public int stateOfUpgrade;
    public int maxUpgradeOfPL;
    public int costOfUpgrade;
    public int warehouseNB = 1;
    public GameObject money;
    public GameObject warehousePlace;

    public Text stateOfUpgradeText;
    public Text maxUpgradeOfPLText;
    public Text costOfUpgradeText;
    // Start is called before the first frame update
    void Start()
    {
        stateOfUpgradeText.text = "Number of upgrade = " + stateOfUpgrade.ToString();
        maxUpgradeOfPLText.text = "Max number of upgrade = " + maxUpgradeOfPL.ToString();
        costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void upgradeWarehouse()
    {

        if (money.GetComponent<MoneyMaking>().getMoney() > costOfUpgrade && stateOfUpgrade < maxUpgradeOfPL) {
            stateOfUpgrade += 1;
            costOfUpgrade *= 2;
            stateOfUpgradeText.text = "Number of upgrade = " + stateOfUpgrade.ToString();
            costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
            money.GetComponent<MoneyMaking>().pay(costOfUpgrade);
            warehousePlace.GetComponent<WarehouseNB>().increaseMaxPlace(5);
        }
    }
}
