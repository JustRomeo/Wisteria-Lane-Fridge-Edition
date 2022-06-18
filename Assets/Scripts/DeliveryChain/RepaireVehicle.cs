using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepaireVehicle : MonoBehaviour
{
    private float timeRemaining;

    public GameObject money;
    public GameObject deliveryChain;
    public GameObject damageToaster;

    private int costOfRepair;

    public Text costOfRepairText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setDeliveryChainObject(GameObject deliveryObject)
    {
        deliveryChain = deliveryObject;
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

    public void repairVehicle()
    {
        if (money.GetComponent<MoneyMaking>().getMoney() > costOfRepair) {
            money.GetComponent<MoneyMaking>().pay(costOfRepair);
            money.GetComponent<MoneyMaking>().resetCarMoneyModifier(deliveryChain, damageToaster, gameObject);
            deliveryChain.GetComponent<DeliveryChain>().stopDamage();
        }
    }
}
