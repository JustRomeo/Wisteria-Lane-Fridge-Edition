using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class SpyEnemyDelivery : MonoBehaviour
{
    private int budget;
    private int spyType;
    private int percent;
    private float budgetMultiplier;

    private int fullBudget;

    public TMP_Text budgetText;
    public TMP_Text percentText;
    public TMP_Text fullBudgetText;
    public Slider budgetSlider;
    public GameObject vehiclesSpyButton;
    public GameObject vehicleAndCapacitySpyButton;
    public GameObject maintenanceSpyButton;

    public GameObject money;
    public GameObject deliveryChain;

    public GameObject toaster;
    public GameObject ToasterList;

    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        budget = 500;
        spyType = 1;
        budgetMultiplier = 1f;
        percent = 24;
        vehiclesSpyButton.GetComponent<Image>().color = Color.red;
        fullBudget = 500;
        fullBudgetText.text = "Total price:\n" + budget.ToString() + " * " + budgetMultiplier.ToString() + " = " + fullBudget.ToString();
        percentText.text = "Success percent = " + percent.ToString();
        view = GetComponent<PhotonView>();
    }

    void catchSpy()
    {
        string toasterTitle = "Someone tried to spy you but failed";

        GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
        spyToaster.GetComponent<AdToaster>().setRemainingTime(5);
        spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
        spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

        ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);
    }

    public void changeSpyBudget()
    {
        budget = (int)(budgetSlider.value) * 500;

        percent = 24 * (int)(budgetSlider.value);

        if (spyType == 2)
            fullBudget = budget * 3;
        if (spyType == 3)
            fullBudget = budget * 2;
        budgetText.text = "= " + budget.ToString();
        percentText.text = "Success percent = " + percent.ToString();
        updateTotalPrice();
    }

    public void updateTotalPrice()
    {
        fullBudget = (int)(budgetMultiplier * budget);
        fullBudgetText.text = "Total price:\n" + budget.ToString() + " * " + budgetMultiplier.ToString() + " = " + fullBudget.ToString();
    }

    public void selectSpyTarget(int target)
    {
        spyType = target;
        if (target == 1) {
            vehiclesSpyButton.GetComponent<Image>().color = Color.red;
            vehicleAndCapacitySpyButton.GetComponent<Image>().color = Color.white;
            maintenanceSpyButton.GetComponent<Image>().color = Color.white;
            budgetMultiplier = 1f;
        }
        if (target == 2) {
            vehiclesSpyButton.GetComponent<Image>().color = Color.white;
            vehicleAndCapacitySpyButton.GetComponent<Image>().color = Color.red;
            maintenanceSpyButton.GetComponent<Image>().color = Color.white;
            budgetMultiplier = 2.5f;
        }
        if (target == 3) {
            vehiclesSpyButton.GetComponent<Image>().color = Color.white;
            vehicleAndCapacitySpyButton.GetComponent<Image>().color = Color.white;
            maintenanceSpyButton.GetComponent<Image>().color = Color.red;
            budgetMultiplier = 2f;
        }
        updateTotalPrice();
    }

    public void sendSpy()
    {
        if (money.GetComponent<MoneyMaking>().getMoney() >= fullBudget) {
            money.GetComponent<MoneyMaking>().pay(fullBudget);
            if (spyType == 1)
                SpyVehicleRequest();
            if (spyType == 2)
                SpyVehicleAndCapacityRequest();
            if (spyType == 3) {
                SpyMaintenance();
            }
        }
    }

    //Spy opponent nb of vehicle
    public void SpyVehicleRequest() {
        view.RPC("SpyVehicleRequestRPC", RpcTarget.All, percent, PlayerManager.playerID);
    }
    [PunRPC]
    void SpyVehicleRequestRPC(int successPercent, int playerid) {
        if (playerid != PlayerManager.playerID) {
            int randomSuccess = Random.Range(1, 100);
            Debug.Log(successPercent.ToString() + " " + percent);
            if (randomSuccess <= successPercent) {
                int nbCar = deliveryChain.GetComponent<DeliveryChain>().getNbOfCar();
                int nbTruck = deliveryChain.GetComponent<DeliveryChain>().getNbOfTruck();
                view.RPC("SpyVehicleSendRPC", RpcTarget.All, nbCar, nbTruck, PlayerManager.playerID);
            }
            else
                view.RPC("SpyFailedRPC", RpcTarget.All, PlayerManager.playerID);
        }
    }
    [PunRPC]
    void SpyVehicleSendRPC(int nbCar, int nbTruck, int playerid) {
        if (playerid != PlayerManager.playerID) {
            string toasterTitle = "Your opponent have " + nbCar.ToString() + " car and " + nbTruck.ToString() + " truck";

            GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
            spyToaster.GetComponent<AdToaster>().setRemainingTime(15);
            spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
            spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

            ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);

            deliveryChain.GetComponent<DeliveryChain>().setOpponentVehicleNb(nbCar, nbTruck);
        }
    }

    //Spy opponent vehicle capacity
    public void SpyVehicleAndCapacityRequest() {
        view.RPC("SpyVehicleAndCapacityRequestRPC", RpcTarget.All, percent, PlayerManager.playerID);
    }
    [PunRPC]
    void SpyVehicleAndCapacityRequestRPC(int successPercent, int playerid) {
        if (playerid != PlayerManager.playerID) {
            int randomSuccess = Random.Range(1, 100);
            Debug.Log(successPercent.ToString() + " " + percent);
            if (randomSuccess <= successPercent) {
                int carCapacity = deliveryChain.GetComponent<DeliveryChain>().getCarTransportCapacity();
                int truckCapacity = deliveryChain.GetComponent<DeliveryChain>().getTruckTransportCapacity();
                int nbCar = deliveryChain.GetComponent<DeliveryChain>().getNbOfCar();
                int nbTruck = deliveryChain.GetComponent<DeliveryChain>().getNbOfTruck();
                view.RPC("SpyVehicleAndCapacitySendRPC", RpcTarget.All, carCapacity, truckCapacity, nbCar, nbTruck, PlayerManager.playerID);
            }
            else
                view.RPC("SpyFailedRPC", RpcTarget.All, PlayerManager.playerID);
        }
    }
    [PunRPC]
    void SpyVehicleAndCapacitySendRPC(int carCapacity, int truckCapacity, int carNb, int truckNb, int playerid) {
        if (playerid != PlayerManager.playerID) {
            string toasterTitle = "Your opponent have " + carNb.ToString() + " car and " + truckNb.ToString() + " truck\nCar capacity: " + carCapacity.ToString() + " Truck capacity: " + truckCapacity.ToString();

            GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
            spyToaster.GetComponent<AdToaster>().setRemainingTime(7);
            spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
            spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

            ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);

            deliveryChain.GetComponent<DeliveryChain>().setOpponentVehicleNb(carNb, truckCapacity);
            deliveryChain.GetComponent<DeliveryChain>().setOpponentVehicleCapacity(carCapacity, truckCapacity);
        }
    }

    //Spy maintenance
    public void SpyMaintenance() {
        view.RPC("SpyMaintenanceRequestRPC", RpcTarget.All, percent, PlayerManager.playerID);
    }
    [PunRPC]
    void SpyMaintenanceRequestRPC(int successPercent, int playerid) {
        if (playerid != PlayerManager.playerID) {
            int randomSuccess = Random.Range(1, 100);
            Debug.Log(successPercent.ToString() + " " + percent);
            if (randomSuccess <= successPercent) {
                int maintenancePercent = deliveryChain.GetComponent<DeliveryChain>().getMaintenancePercent();
                view.RPC("SpyMaintenanceSendRPC", RpcTarget.All, maintenancePercent, PlayerManager.playerID);
            }
            else
                view.RPC("SpyFailedRPC", RpcTarget.All, PlayerManager.playerID);
        }
    }
    [PunRPC]
    void SpyMaintenanceSendRPC(int maintenacePercent, int playerid) {
        if (playerid != PlayerManager.playerID) {
            string toasterTitle = "Your opponent's maintenance percent: " + ((int)(maintenacePercent * 10)).ToString() + "%";

            GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
            spyToaster.GetComponent<AdToaster>().setRemainingTime(7);
            spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
            spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

            ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);

            deliveryChain.GetComponent<DeliveryChain>().setMaintenancePercent(maintenacePercent);
        }
    }

    //Failed attempt
    [PunRPC]
    void SpyFailedRPC(int playerid) {
        if (playerid != PlayerManager.playerID) {
            string toasterTitle = "Your spy attemps have failed";

            GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
            spyToaster.GetComponent<AdToaster>().setRemainingTime(7);
            spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
            spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

            ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);
        }
    }

}
