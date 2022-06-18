using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class SabotageProduction : MonoBehaviour
{
    private int budget;
    private int sabotageType;
    private int percent;
    private float budgetMultiplier;

    private int fullBudget;

    public TMP_Text budgetText;
    public TMP_Text percentText;
    public TMP_Text fullBudgetText;
    public Slider budgetSlider;
    public GameObject type1DamageButton;
    public GameObject type2DamageButton;
    public GameObject type3DamageButton;

    public GameObject money;
    public GameObject productionLine;

    public GameObject toaster;
    public GameObject ToasterList;

    public PhotonView view;

    public AudioClip clip;
    public float volume = 10;

    // Start is called before the first frame update
    void Start()
    {
        budget = 500;
        percent = 17;
        budgetMultiplier = 1.5f;
        sabotageType = 1;
        type1DamageButton.GetComponent<Image>().color = Color.red;
        fullBudget = (int)(budget * budgetMultiplier);
        fullBudgetText.text = "Total price:\n" + budget.ToString() + " * " + budgetMultiplier.ToString() + " = " + fullBudget.ToString();
        percentText.text = "Success percent = " + percent.ToString();
        view = GetComponent<PhotonView>();
    }

    void catchSabotage()
    {
        string toasterTitle = "Someone tried to sabotage you but failed";

        GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
        spyToaster.GetComponent<AdToaster>().setRemainingTime(10);
        spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
        spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

        ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);
    }

    public void changeSabotageBudget()
    {
        budget = (int)(budgetSlider.value) * 500;

        percent = 17 * (int)(budgetSlider.value);

        budgetText.text = "= " + budget.ToString();
        percentText.text = "Success percent = " + percent.ToString();
        updateTotalPrice();
    }

    public void updateTotalPrice()
    {
        fullBudget = (int)(budget * budgetMultiplier);
        fullBudgetText.text = "Total price:\n" + budget.ToString() + " * " + budgetMultiplier.ToString() + " = " + fullBudget.ToString();
    }

    public void selectSabotageType(int target)
    {
        sabotageType = target;
        if (target == 1) {
            type1DamageButton.GetComponent<Image>().color = Color.red;
            type2DamageButton.GetComponent<Image>().color = Color.white;
            type3DamageButton.GetComponent<Image>().color = Color.white;
            budgetMultiplier = 1.5f;
        }
        if (target == 2) {
            type1DamageButton.GetComponent<Image>().color = Color.white;
            type2DamageButton.GetComponent<Image>().color = Color.red;
            type3DamageButton.GetComponent<Image>().color = Color.white;
            budgetMultiplier = 3.75f;
        }
        if (target == 3) {
            type1DamageButton.GetComponent<Image>().color = Color.white;
            type2DamageButton.GetComponent<Image>().color = Color.white;
            type3DamageButton.GetComponent<Image>().color = Color.red;
            budgetMultiplier = 6f;
        }
        updateTotalPrice();
    }

    public void sabotage()
    {
        if (money.GetComponent<MoneyMaking>().getMoney() >= fullBudget) {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            money.GetComponent<MoneyMaking>().pay(fullBudget);
            view.RPC("SabotageRequestRPC", RpcTarget.All, percent, sabotageType, PlayerManager.playerID);
        }
    }

    [PunRPC]
    void SabotageRequestRPC(int successPercent, int type, int playerid) {
        if (playerid != PlayerManager.playerID) {
            int randomSuccess = Random.Range(1, 100);
            Debug.Log(successPercent.ToString() + " " + percent);

            if (randomSuccess <= successPercent) {
                bool carDamage = productionLine.GetComponent<ProductionManagement>().isProductionDamaged();
                if (carDamage == false)
                    productionLine.GetComponent<ProductionManagement>().productionSabotage(type);
                view.RPC("SabotageSuccessSendRPC", RpcTarget.All, carDamage, PlayerManager.playerID);
            }
            else {
                catchSabotage();
                view.RPC("SabotageFailSendRPC", RpcTarget.All, PlayerManager.playerID);
            }
        }
    }

    [PunRPC]
    void SabotageSuccessSendRPC(bool carDamge, int playerid) {
        if (playerid != PlayerManager.playerID) {
            string toasterTitle = "";
            if (carDamge == false)
                toasterTitle = "Your sabotage attemps have succeed";
            else {
                toasterTitle = "Opponent car already damaged, can't sabotage.\nSabotage cost half refunded";
                money.GetComponent<MoneyMaking>().addMoney(fullBudget / 2);
            }

            GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
            spyToaster.GetComponent<AdToaster>().setRemainingTime(5);
            spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
            spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

            ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);
        }
    }

    [PunRPC]
    void SabotageFailSendRPC(int playerid) {
        if (playerid != PlayerManager.playerID) {
            string toasterTitle = "Your sabotage attemps have failed";

            GameObject spyToaster = Instantiate(toaster, new Vector3(610, 310, 0), Quaternion.identity);
            spyToaster.GetComponent<AdToaster>().setRemainingTime(5);
            spyToaster.GetComponent<AdToaster>().setAdTitle(toasterTitle);
            spyToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

            ToasterList.GetComponent<ToasterList>().addToaster(spyToaster);
        }
    }
}
