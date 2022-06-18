using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyMaking : MonoBehaviour
{

    public int money = 5000;
    public Text moneyText;
    public int moneyPerHour;
    public int enterpriseValue;

    private int opponentMoney;
    private int opponentMoneyPerHour;

    private int nextMoneyPerHour;
    public Text moneyPerHourText;

    private float carBudgetTime;
    private float prodBudgetTime;

    private bool isCarBudgetPaymentActive;
    private bool isProdBudgetPaymentActive;

    private float permanentModifier;

    private float adModifierTime;
    private float adModifier;
    private bool isAdModifierActive;

    private float carDamageModifierTime;
    private float carDamageModifier;
    private bool isCarDamageModifierActive;

    private float prodDamageModifierTime;
    private float prodDamageModifier;
    private bool isProdDamageModifierActive;

    private int carMaintenanceBudget = 0;
    private int prodMaintenanceBudget = 0;

    public GameObject adPrefab;
    public GameObject carRepairButtonPrefab;
    public GameObject productionRepairButtonPrefab;
    public GameObject Canvas;
    public GameObject carRepairCanvas;
    public GameObject prodRepairCanvas;
    public GameObject ToasterList;
    // Start is called before the first frame update
    void Start()
    {
        isAdModifierActive = false;
        adModifier = 1;
        adModifierTime = 1;

        isCarDamageModifierActive = false;
        carDamageModifier = 1;
        carDamageModifierTime = 1;

        isProdDamageModifierActive = false;
        prodDamageModifier = 1;
        prodDamageModifierTime = 1;

        moneyPerHour = 750;
        moneyText.text = money.ToString() + "$";
        moneyPerHourText.text = moneyPerHour.ToString() + "$/h";
        carBudgetTime = 0;
        prodBudgetTime = 0;

        isCarBudgetPaymentActive = false;
        isProdBudgetPaymentActive = false;

        opponentMoney = 0;
        opponentMoneyPerHour = 0;

        enterpriseValue = money;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("enterpriseValue", enterpriseValue);

        permanentModifier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        carBudgetTime += deltaTime;
        prodBudgetTime += deltaTime;

        if (carBudgetTime > 15 && isCarBudgetPaymentActive == true) {
            money -= carMaintenanceBudget;
            PlayerPrefs.SetInt("money", money);
            moneyText.text = money.ToString() + "$";
            PlayerPrefs.SetInt("enterpriseValue", enterpriseValue);
            carBudgetTime = 0;
        }
        if (prodBudgetTime > 15 && isProdBudgetPaymentActive == true) {
            money -= prodMaintenanceBudget;
            PlayerPrefs.SetInt("money", money);
            moneyText.text = money.ToString() + "$";
            PlayerPrefs.SetInt("enterpriseValue", enterpriseValue);
            prodBudgetTime = 0;
        }
    }

    public void earnMoney(int moneyEarned)
    {
        //change money earned with money modifier : advertising and damage
        if (isAdModifierActive == true)
            moneyEarned = (int)(moneyEarned * adModifier * permanentModifier);

        if (isCarDamageModifierActive == true)
            moneyEarned = (int)(moneyEarned * carDamageModifier * permanentModifier);

        if (isProdDamageModifierActive == true)
            moneyEarned = (int)(moneyEarned * prodDamageModifier * permanentModifier);

        money += moneyEarned;
        enterpriseValue += moneyEarned;
        PlayerPrefs.SetInt("money", money);
        moneyText.text = money.ToString() + "$";
        PlayerPrefs.SetInt("enterpriseValue", enterpriseValue);
    }

    public void addMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        PlayerPrefs.SetInt("money", money);
        moneyText.text = money.ToString() + "$";
        PlayerPrefs.SetInt("enterpriseValue", enterpriseValue);
    }

    public void increaseMoneyEarning(int newMoneyPerHour)
    {
        nextMoneyPerHour = (int)(newMoneyPerHour * adModifier * carDamageModifier * prodDamageModifier * permanentModifier);
        moneyPerHour = nextMoneyPerHour;
        moneyPerHourText.text = nextMoneyPerHour.ToString() + "$/h";
    }

    public void resetCarMoneyModifier(GameObject deliveryChain, GameObject damageToaster, GameObject repairButton)
    {
        isCarDamageModifierActive = false;
        carDamageModifierTime = 0;
        carDamageModifier = 1;
        isCarBudgetPaymentActive = true;
        carBudgetTime = 0;
        damageToaster.GetComponent<AdToaster>().destroyAndUpdateList();
        Destroy(repairButton);
        StopCoroutine(coroutineStopCarDamage(deliveryChain, damageToaster, repairButton));
    }

    public void resetProdMoneyModifier(GameObject productionChain, GameObject damageToaster, GameObject repairButton)
    {
        isProdDamageModifierActive = false;
        prodDamageModifierTime = 0;
        prodDamageModifier = 1;
        isProdBudgetPaymentActive = true;
        prodBudgetTime = 0;
        damageToaster.GetComponent<AdToaster>().destroyAndUpdateList();
        Destroy(repairButton);
        StopCoroutine(coroutineStopProdDamage(productionChain, damageToaster, repairButton));
    }

    IEnumerator coroutineStopCarDamage(GameObject deliveryChain, GameObject damageToaster, GameObject repairButton)
    {
        yield return new WaitForSeconds(carDamageModifierTime);
        deliveryChain.GetComponent<DeliveryChain>().stopDamage();
        Destroy(repairButton);

        isCarDamageModifierActive = false;
        carDamageModifierTime = 0;
        carDamageModifier = 1;
        isCarBudgetPaymentActive = true;
        carBudgetTime = 0;
    }

    IEnumerator coroutineStopProdDamage(GameObject productionChain, GameObject damageToaster, GameObject repairButton)
    {
        yield return new WaitForSeconds(prodDamageModifierTime);
        productionChain.GetComponent<ProductionManagement>().stopDamage();
        Destroy(repairButton);

        isProdDamageModifierActive = false;
        prodDamageModifierTime = 0;
        prodDamageModifier = 1;
        isProdBudgetPaymentActive = true;
        prodBudgetTime = 0;
    }

    IEnumerator coroutineStopAd()
    {
        yield return new WaitForSeconds(adModifierTime);
        isAdModifierActive = false;
        adModifier = 1;
    }
    public void setProdMaintenanceBudget(int budget)
    {
        carMaintenanceBudget = budget;
    }
    public void setCarMaintenanceBudget(int budget)
    {
        prodMaintenanceBudget = budget;
    }

    public int getMoney()
    {
        return (money);
    }

    public int getMoneyPerHour()
    {
        return (moneyPerHour);
    }

    public void setCarMaintenancePayment()
    {
        isCarBudgetPaymentActive = true;
        carBudgetTime = 0;
    }

    public void setProdMaintenancePayment()
    {
        isProdBudgetPaymentActive = true;
        prodBudgetTime = 0;
    }

    public void setCommunicationActive()
    {
        permanentModifier += 0.30F;
    }

    public void setSpyActive()
    {
        permanentModifier += 0.20F;
    }

    public void setSabotageActive()
    {
        permanentModifier += 0.20F;
    }

    public int getEnterpriseValue() {return enterpriseValue;}

    public void pay(int valueToPay)
    {
        money -= valueToPay;
        PlayerPrefs.SetInt("money", money);
        moneyText.text = money.ToString() + "$";
    }

    public void failedAdCreation(float timeActive)
    {
        string adTitle = "The launch of the advertising campaign failed";

        GameObject newAd = Instantiate(adPrefab, new Vector3(610, 310, 0), Quaternion.identity);

        newAd.GetComponent<AdToaster>().setRemainingTime(timeActive);
        newAd.GetComponent<AdToaster>().setAdTitle(adTitle);
        newAd.GetComponent<AdToaster>().setToasterList(ToasterList);

        ToasterList.GetComponent<ToasterList>().addToaster(newAd);
    }

    public void createAdd(float timeBoost, float moneyBoost, string adName)
    {
        isAdModifierActive = true;
        adModifier = moneyBoost;
        adModifierTime = timeBoost;
        createAdInfoCanvas(adName);
    }

    void createAdInfoCanvas(string adName)
    {
        string adTitle = "Campain \"" + adName + "\" launched\nBoosting money received";

        GameObject newAd = Instantiate(adPrefab, new Vector3(610, 310, 0), Quaternion.identity);

        newAd.GetComponent<AdToaster>().setRemainingTime(adModifierTime);
        newAd.GetComponent<AdToaster>().setAdTitle(adTitle);
        newAd.GetComponent<AdToaster>().setToasterList(ToasterList);

        ToasterList.GetComponent<ToasterList>().addToaster(newAd);
        StartCoroutine(coroutineStopAd());
    }

    public void createCarDamageToaster(VehicleDamageStat damageStat, GameObject deliveryChain)
    {
        isCarDamageModifierActive = true;
        isCarBudgetPaymentActive = false;

        carDamageModifier = damageStat.getMoneyDrop();
        carDamageModifierTime = damageStat.getTimeDuration();

        int moneyReduction = (int)((1 - damageStat.getMoneyDrop()) * 100);

        GameObject damageToaster = Instantiate(adPrefab, new Vector3(610, 310, 0), Quaternion.identity);
        damageToaster.GetComponent<AdToaster>().setRemainingTime(damageStat.getTimeDuration());
        damageToaster.GetComponent<AdToaster>().setAdTitle("Your vehicles are damaged.\nMoney earning reduced by " + moneyReduction.ToString() + "%");
        damageToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

        ToasterList.GetComponent<ToasterList>().addToaster(damageToaster);

        GameObject damageRepairButton = Instantiate(carRepairButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        damageRepairButton.transform.SetParent(carRepairCanvas.transform, false);
        damageRepairButton.GetComponent<RepaireVehicle>().setDeliveryChainObject(deliveryChain);
        damageRepairButton.GetComponent<RepaireVehicle>().setMoneyObject(gameObject);
        damageRepairButton.GetComponent<RepaireVehicle>().setDamageToasterObject(damageToaster);
        damageRepairButton.GetComponent<RepaireVehicle>().setButtonTitle("Repair your vehicles for : " + damageStat.getRepairCost().ToString() + "$");
        damageRepairButton.GetComponent<RepaireVehicle>().setRemainingTime(damageStat.getTimeDuration());
        damageRepairButton.GetComponent<RepaireVehicle>().setRepairCost(damageStat.getRepairCost());

        StartCoroutine(coroutineStopCarDamage(deliveryChain, damageToaster, damageRepairButton));
    }

    public void createProductionDamageToaster(VehicleDamageStat damageStat, GameObject productionChain)
    {
        isProdDamageModifierActive = true;
        isProdBudgetPaymentActive = false;

        prodDamageModifier = damageStat.getMoneyDrop();
        prodDamageModifierTime = damageStat.getTimeDuration();

        int moneyReduction = (int)((1 - damageStat.getMoneyDrop()) * 100);

        GameObject damageToaster = Instantiate(adPrefab, new Vector3(610, 310, 0), Quaternion.identity);
        damageToaster.GetComponent<AdToaster>().setRemainingTime(damageStat.getTimeDuration());
        damageToaster.GetComponent<AdToaster>().setAdTitle("Your production chain is damaged.\nMoney earning reduced by " + moneyReduction.ToString() + "%");
        damageToaster.GetComponent<AdToaster>().setToasterList(ToasterList);

        ToasterList.GetComponent<ToasterList>().addToaster(damageToaster);

        GameObject damageRepairButton = Instantiate(productionRepairButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        damageRepairButton.transform.SetParent(prodRepairCanvas.transform, false);
        damageRepairButton.GetComponent<RepairProductionLine>().setProductionLineObject(productionChain);
        damageRepairButton.GetComponent<RepairProductionLine>().setMoneyObject(gameObject);
        damageRepairButton.GetComponent<RepairProductionLine>().setDamageToasterObject(damageToaster);
        damageRepairButton.GetComponent<RepairProductionLine>().setButtonTitle("Repair your production chain for : " + damageStat.getRepairCost().ToString() + "$");
        damageRepairButton.GetComponent<RepairProductionLine>().setRemainingTime(damageStat.getTimeDuration());
        damageRepairButton.GetComponent<RepairProductionLine>().setRepairCost(damageStat.getRepairCost());

        StartCoroutine(coroutineStopProdDamage(productionChain, damageToaster, damageRepairButton));
    }

    public void setOpponentMoney(int opponentSpyedMoney)
    {
        opponentMoney = opponentSpyedMoney;
    }

    public void setOpponentProduction(int opponentSpyedMoneyPerHour)
    {
        opponentMoneyPerHour = opponentSpyedMoneyPerHour;
    }
}
