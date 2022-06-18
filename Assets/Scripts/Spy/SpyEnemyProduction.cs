using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class SpyEnemyProduction : MonoBehaviour
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
    public GameObject moneySpyButton;
    public GameObject productionSpyButton;
    public GameObject moneyAndProductionSpyButton;

    public GameObject money;
    public GameObject productionLine;

    public GameObject toaster;
    public GameObject ToasterList;

    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        budget = 500;
        spyType = 1;
        budgetMultiplier = 1;
        percent = 24;
        moneySpyButton.GetComponent<Image>().color = Color.red;
        fullBudget = 500;
        fullBudgetText.text = "Total price:\n" + budget.ToString() + " * " + budgetMultiplier.ToString() + " = " + fullBudget.ToString();
        percentText.text = "Success percent = " + percent.ToString();
        view = GetComponent<PhotonView>();
    }

    void catchSpy()
    {
        string toasterTitle = "Someone tried to spy you but failed";

        GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
        spyToaster.GetComponent<AdToaster>().setRemainingTime(6);
        spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
        spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

        ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);
    }

    public void changeSpyBudget()
    {
        budget = (int)(budgetSlider.value) * 500;

        percent = 24 * (int)(budgetSlider.value);

        if (spyType == 1 || spyType == 2)
            fullBudget = budget;
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
            moneySpyButton.GetComponent<Image>().color = Color.red;
            productionSpyButton.GetComponent<Image>().color = Color.white;
            moneyAndProductionSpyButton.GetComponent<Image>().color = Color.white;
            fullBudget = 1 * budget;
            budgetMultiplier = 1f;
        }
        if (target == 2) {
            moneySpyButton.GetComponent<Image>().color = Color.white;
            productionSpyButton.GetComponent<Image>().color = Color.red;
            moneyAndProductionSpyButton.GetComponent<Image>().color = Color.white;
            budgetMultiplier = 1f;
        }
        if (target == 3) {
            moneySpyButton.GetComponent<Image>().color = Color.white;
            productionSpyButton.GetComponent<Image>().color = Color.white;
            moneyAndProductionSpyButton.GetComponent<Image>().color = Color.red;
            budgetMultiplier = 1.5f;
        }
        updateTotalPrice();
    }

    public void sendSpy()
    {
        if (money.GetComponent<MoneyMaking>().getMoney() >= fullBudget) {
            money.GetComponent<MoneyMaking>().pay(fullBudget);
            if (spyType == 1)
                SpyMoneyRequest();
            if (spyType == 2)
                SpyFridgeProductionRequest();
            if (spyType == 3) {
                SpyMoneyRequest();
                SpyFridgeProductionRequest();
            }
        }
    }

    public void SpyMoneyRequest() {
        view.RPC("SpyMoneyRequestRPC", RpcTarget.All, percent, PlayerManager.playerID);
    }

    public void SpyFridgeProductionRequest() {
        view.RPC("SpyFridgeProductionRequestRPC", RpcTarget.All, percent, PlayerManager.playerID);
    }


    [PunRPC]
    void SpyMoneyRequestRPC(int successPercent, int playerid) {
        Debug.Log(playerid.ToString() + " != " + PlayerManager.playerID);
        if (playerid != PlayerManager.playerID) {
            int randomSuccess = Random.Range(1, 100);
            Debug.Log(successPercent.ToString() + " " + percent);
            if (randomSuccess <= successPercent)
                view.RPC("SpyMoneySendRPC", RpcTarget.All, money.GetComponent<MoneyMaking>().getMoney(), money.GetComponent<MoneyMaking>().getMoneyPerHour(), PlayerManager.playerID);
            else
                view.RPC("SpyFailedRPC", RpcTarget.All, PlayerManager.playerID);
        }
    }

    [PunRPC]
    void SpyFridgeProductionRequestRPC(int successPercent, int playerid) {
        if (playerid != PlayerManager.playerID) {
            int randomSuccess = Random.Range(1, 100);
            if (randomSuccess <= successPercent) {
                int fridgePrice = productionLine.GetComponent<ProductionManagement>().getFridgePrice();
                int fridgeProduction = productionLine.GetComponent<ProductionManagement>().getFridgeProduction();
                view.RPC("SpyFridgeProductionRPC", RpcTarget.All, fridgePrice, fridgeProduction, PlayerManager.playerID);
            }
            else
                view.RPC("SpyFailedRPC", RpcTarget.All, PlayerManager.playerID);
        }
    }

    [PunRPC]
    void SpyMoneySendRPC(int opponentMoney, int opponentMoneyPerHour, int playerid) {
        if (playerid != PlayerManager.playerID) {
            string toasterTitle = "Your opponent have " + opponentMoney.ToString() + "$\nYour opponent produce " + opponentMoneyPerHour.ToString() + "/h";

            GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
            spyToaster.GetComponent<AdToaster>().setRemainingTime(7);
            spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
            spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

            ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);

            money.GetComponent<MoneyMaking>().setOpponentMoney(opponentMoney);
            money.GetComponent<MoneyMaking>().setOpponentProduction(opponentMoneyPerHour);
        }
    }

    [PunRPC]
    void SpyFridgeProductionRPC(int fridgePrice, int fridgeProduction, int playerid) {
        if (playerid != PlayerManager.playerID) {
            // PlayerManager.opponentFridgeProduction = fridgeProduction;

            string toasterTitle = "Opponent fridge price " + fridgePrice.ToString() + "$\nOpponent fridge production " + fridgeProduction.ToString() + "/h";

            GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
            spyToaster.GetComponent<AdToaster>().setRemainingTime(7);
            spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
            spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

            ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);

            productionLine.GetComponent<ProductionManagement>().setOpponentFridgePrice(fridgePrice);
            productionLine.GetComponent<ProductionManagement>().setOpponentFridgeProduction(fridgeProduction);
        }
    }

    [PunRPC]
    void SpyFailedRPC(int playerid) {
        if (playerid != PlayerManager.playerID) {
            string toasterTitle = "Your spy attemps have failed";

            GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
            spyToaster.GetComponent<AdToaster>().setRemainingTime(15);
            spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
            spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

            ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);
        }
    }
}
