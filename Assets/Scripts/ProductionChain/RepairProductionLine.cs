using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairProductionLine : MonoBehaviour
{
    private float timeRemaining;

    public GameObject money;
    public GameObject productionLine;
    public GameObject damageToaster;

    private int costOfRepair;

    public Text costOfRepairText;

    void Start()
    {
    }

    public void setProductionLineObject(GameObject productionObject)
    {
        productionLine = productionObject;
    }

    public void setMoneyObject(GameObject moneyObject)
    {
        money = moneyObject;
    }

    public void setDamageToasterObject(GameObject damageToasterObject)
    {
        damageToaster = damageToasterObject;
    }

    public void setRemainingTime(float time)
    {
        timeRemaining = time;
    }

    public void setButtonTitle(string buttonTitle)
    {
        costOfRepairText.text = buttonTitle;
    }

    public void setRepairCost(int repairCost)
    {
        costOfRepair = repairCost;
    }

    public void repairProductionLine()
    {
        if (money.GetComponent<MoneyMaking>().getMoney() > costOfRepair) {
            money.GetComponent<MoneyMaking>().pay(costOfRepair);
            money.GetComponent<MoneyMaking>().resetProdMoneyModifier(productionLine, damageToaster, gameObject);
            productionLine.GetComponent<ProductionManagement>().stopDamage();
        }
    }
}
