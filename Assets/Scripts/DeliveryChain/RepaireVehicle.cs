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

    private int costOfUpgrade;

    public Text costOfUpgradeText;
    // Start is called before the first frame update
    void Start()
    {
        costOfUpgrade = 500;
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
        costOfUpgradeText.text = buttonTitle;
    }

    public void repairVehicle()
    {
        if (money.GetComponent<MoneyMaking>().getMoney() > costOfUpgrade) {
            money.GetComponent<MoneyMaking>().pay(costOfUpgrade);
            money.GetComponent<MoneyMaking>().resetMoneyModifier(deliveryChain, damageToaster, gameObject);
            deliveryChain.GetComponent<DeliveryChain>().stopDamage();
        }
    }
}
