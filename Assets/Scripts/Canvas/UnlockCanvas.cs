using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCanvas : MonoBehaviour
{
    public GameObject money;

    public GameObject deliveryCanvas;
    public GameObject communicationCanvas;
    public GameObject spyCanvas;
    public GameObject sabotageCanvas;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void unlockDelivery(int price)
    {
        if (money.GetComponent<MoneyMaking>().getMoney() > price) {
            money.GetComponent<MoneyMaking>().pay(price);
            money.GetComponent<MoneyMaking>().setCarMaintenancePayment();
            deliveryCanvas.GetComponent<DeliveryChain>().setActiveDelivery();
        }
    }

    public void unlockCommunication(int price) {
        if (money.GetComponent<MoneyMaking>().getMoney() > price) {
            money.GetComponent<MoneyMaking>().pay(price);
            money.GetComponent<MoneyMaking>().setCommunicationActive();
            communicationCanvas.GetComponent<CreateAdvertisement>().unlockAdvertisement();
        }
    }

    public void unlockSpy(int price) {
        if (money.GetComponent<MoneyMaking>().getMoney() > price) {
            money.GetComponent<MoneyMaking>().pay(price);
            money.GetComponent<MoneyMaking>().setSpyActive();
            spyCanvas.GetComponent<SpyCanvas>().unlockSpy();
        }
    }

    public void unlockSabotage(int price) {
        if (money.GetComponent<MoneyMaking>().getMoney() > price) {
            money.GetComponent<MoneyMaking>().pay(price);
            money.GetComponent<MoneyMaking>().setSabotageActive();
            sabotageCanvas.GetComponent<SabotageCanvas>().unlockSabotage();
        }
    }
}
