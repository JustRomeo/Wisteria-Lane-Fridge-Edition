using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyNewCar : MonoBehaviour
{
    private int nbCar;
    public int costOfUpgrade;

    public GameObject money;
    public GameObject delivery;

    public Text costOfUpgradeText;
    // Start is called before the first frame update
    void Start()
    {
        nbCar = 2;
        costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
    }

    public void buyCar()
    {
        nbCar = delivery.GetComponent<DeliveryChain>().getNbOfCar();
        int maxNbOfCar = delivery.GetComponent<DeliveryChain>().getMaxNbOfCar();
        if (money.GetComponent<MoneyMaking>().getMoney() > costOfUpgrade && nbCar < maxNbOfCar) {
            money.GetComponent<MoneyMaking>().pay(costOfUpgrade);
            delivery.GetComponent<DeliveryChain>().addCar();
            nbCar += 1;
            costOfUpgrade += 2000;
            costOfUpgradeText.text = "Cost of upgrade = " + costOfUpgrade.ToString() + "$";
        }
    }
}
