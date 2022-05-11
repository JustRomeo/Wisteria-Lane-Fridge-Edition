using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyMaking : MonoBehaviour
{

    public int money;
    public Text moneyText;
    public int moneyPerHour;
    public int enterpriseValue;

    private int nextMoneyPerHour;
    public Text moneyPerHourText;

    private float time;
    private float budgetTime;

    private bool isBudgetPaymentActive;

    private float moneyModifierTime;
    private float moneyModifier;
    private bool isMoneyModifierActive;

    private int maintenanceBudget = 0;

    public GameObject adPrefab;
    public GameObject repairButtonPrefab;
    public GameObject Canvas;
    public GameObject RepairCanvas;
    // Start is called before the first frame update
    void Start()
    {
        isMoneyModifierActive = false;
        moneyModifier = 1;
        moneyModifierTime = 1;

        money = 5000;
        moneyPerHour = 750;
        moneyText.text = money.ToString() + "$";
        moneyPerHourText.text = moneyPerHour.ToString() + "$/h";
        time = 0;
        budgetTime = 0;
        isBudgetPaymentActive = true;

        enterpriseValue = money;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("enterpriseValue", enterpriseValue);
    }

    // Update is called once per frame
    void Update()
    {
        budgetTime += Time.deltaTime;

        if (budgetTime > 7 && isBudgetPaymentActive == true) {
            // Debug.Log("Money before = " + money.ToString() + "; money after = " + (money - maintenanceBudget).ToString());
            Debug.Log(maintenanceBudget);
            money -= maintenanceBudget;
            PlayerPrefs.SetInt("money", money);
            moneyText.text = money.ToString() + "$";
            PlayerPrefs.SetInt("enterpriseValue", enterpriseValue);
            budgetTime = 0;
        }
    }

    public void earnMoney(int moneyEarned)
    {
        time += Time.deltaTime;
        if (isMoneyModifierActive == true && time < moneyModifierTime) {
            moneyEarned = (int)(moneyEarned * moneyModifier);
        }
        if (time > moneyModifierTime) {
            isMoneyModifierActive = false;
            time = 0;
            moneyModifier = 1;
        }
        money += moneyEarned;
        enterpriseValue += moneyEarned;
        PlayerPrefs.SetInt("money", money);
        moneyText.text = money.ToString() + "$";
        PlayerPrefs.SetInt("enterpriseValue", enterpriseValue);
    }

    public void increaseMoneyEarning(int newMoneyPerHour)
    {
        time += Time.deltaTime;
        float boost = 1;
        if (isMoneyModifierActive == true && time < moneyModifierTime) {
            boost = moneyModifier;
        }
        if (time > moneyModifierTime) {
            isMoneyModifierActive = false;
            time = 0;
            moneyModifier = 1;
        }
        nextMoneyPerHour = (int)(newMoneyPerHour * boost);
        moneyPerHourText.text = nextMoneyPerHour.ToString() + "$/h";
    }

    public void resetMoneyModifier(GameObject deliveryChain, GameObject damageToaster, GameObject repairButton)
    {
        isMoneyModifierActive = false;
        moneyModifierTime = 0;
        moneyModifier = 1;
        isBudgetPaymentActive = true;
        budgetTime = 0;
        Destroy(damageToaster);
        Destroy(repairButton);
        StopCoroutine(coroutineStopDamage(deliveryChain, damageToaster, repairButton));
    }

    IEnumerator coroutineStopDamage(GameObject deliveryChain, GameObject damageToaster, GameObject repairButton)
    {
        yield return new WaitForSeconds(moneyModifierTime);
        deliveryChain.GetComponent<DeliveryChain>().stopDamage();
        Destroy(damageToaster);
        Destroy(repairButton);

        isMoneyModifierActive = false;
        moneyModifierTime = 0;
        moneyModifier = 1;
        isBudgetPaymentActive = true;
        budgetTime = 0;
    }

    public void setMaintenanceBudget(int budget)
    {
        maintenanceBudget = budget;
    }

    public int getMoney()
    {
        return (money);
    }

    public int getEnterpriseValue() {return enterpriseValue;}

    public void pay(int valueToPay)
    {
        money -= valueToPay;
        PlayerPrefs.SetInt("money", money);
        moneyText.text = money.ToString() + "$";
    }

    public void createAdd(float timeBoost, float moneyBoost, string adName)
    {
        time = 0;
        isMoneyModifierActive = true;
        moneyModifier = moneyBoost;
        moneyModifierTime = timeBoost;
        createAdInfoCanvas(adName);
    }

    void createAdInfoCanvas(string adName)
    {
        string adTitle = "campain \"" + adName + "\" launched\nBoosting money received";

        GameObject newAd = Instantiate(adPrefab, new Vector3(610, 310, 0), Quaternion.identity);

        newAd.transform.SetParent(Canvas.transform, false);
        newAd.GetComponent<AdToaster>().setRemainingTime(moneyModifierTime);
        newAd.GetComponent<AdToaster>().setAdTitle(adTitle);
    }

    public void createDamageToaster(int damageType, float moneyDrop, float timeDuration, GameObject deliveryChain)
    {
        time = 0;
        isMoneyModifierActive = true;
        isBudgetPaymentActive = false;

        moneyModifier = moneyDrop;
        moneyModifierTime = timeDuration;

        int moneyReduction = (int)((1 - moneyDrop) * 100);

        GameObject damageToaster = Instantiate(adPrefab, new Vector3(610, 310, 0), Quaternion.identity);
        damageToaster.transform.SetParent(Canvas.transform, false);
        damageToaster.GetComponent<AdToaster>().setRemainingTime(timeDuration);
        damageToaster.GetComponent<AdToaster>().setAdTitle("Your vehicles are damaged.\nMoney earning reduced by " + moneyReduction.ToString() + "%");

        GameObject damageRepairButton = Instantiate(repairButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        damageRepairButton.transform.SetParent(RepairCanvas.transform, false);
        damageRepairButton.GetComponent<RepaireVehicle>().setDeliveryChainObject(deliveryChain);
        damageRepairButton.GetComponent<RepaireVehicle>().setMoneyObject(gameObject);
        damageRepairButton.GetComponent<RepaireVehicle>().setDamageToasterObject(damageToaster);
        damageRepairButton.GetComponent<RepaireVehicle>().setButtonTitle("Repair your vehicles for : 500$");
        damageRepairButton.GetComponent<RepaireVehicle>().setRemainingTime(timeDuration);

        StartCoroutine(coroutineStopDamage(deliveryChain, damageToaster, damageRepairButton));
    }
}
