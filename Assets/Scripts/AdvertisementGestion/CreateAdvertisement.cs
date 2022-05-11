using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CreateAdvertisement : MonoBehaviour
{
    public TMP_InputField adName;

    private float adBudget;
    private string adBoostType;
    public Slider adBudgetSlider;
    public TMP_Text adBudgetText;

    private float campainDuration;

    private int adCampainTotalPrice;
    public TMP_Text adCampainTotalPriceText;

    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;

    public GameObject MoneyBoostType;
    public GameObject SpeedBoostType;

    public GameObject money;
    public GameObject productionManagement;

    void Start()
    {
        adBudget = 250;
        campainDuration = 1.5F;
        Button1.GetComponent<Image>().color = Color.red;
        MoneyBoostType.GetComponent<Image>().color = Color.red;
        EventSystem.current.SetSelectedGameObject(Button1);
        EventSystem.current.SetSelectedGameObject(MoneyBoostType);
        adBoostType = "money";
        updateTotalPrice();
    }

    public void DaysDuration(float multiplier)
    {
        campainDuration = multiplier;
        if (multiplier == 1.5) {
            Button1.GetComponent<Image>().color = Color.red;
            Button2.GetComponent<Image>().color = Color.white;
            Button3.GetComponent<Image>().color = Color.white;
        }
        if (multiplier == 2) {
            Button1.GetComponent<Image>().color = Color.white;
            Button2.GetComponent<Image>().color = Color.red;
            Button3.GetComponent<Image>().color = Color.white;
        }
        if (multiplier == 4) {
            Button1.GetComponent<Image>().color = Color.white;
            Button2.GetComponent<Image>().color = Color.white;
            Button3.GetComponent<Image>().color = Color.red;
        }
        updateTotalPrice();
    }

    public void changeCampainBudget()
    {
        adBudget = adBudgetSlider.value * 100;
        adBudgetText.text = adBudget.ToString() + "$";
        updateTotalPrice();
    }

    void updateTotalPrice()
    {
        adCampainTotalPrice = (int)(adBudget * campainDuration);
        adCampainTotalPriceText.text = (adBudget * campainDuration).ToString();
    }

    public void setAdBoostType(string boostType)
    {
        if (boostType == "money") {
            adBoostType = "money";
            SpeedBoostType.GetComponent<Image>().color = Color.white;
            MoneyBoostType.GetComponent<Image>().color = Color.red;
        }
        if (boostType == "speed") {
            adBoostType = "speed";
            MoneyBoostType.GetComponent<Image>().color = Color.white;
            SpeedBoostType.GetComponent<Image>().color = Color.red;
        }
    }

    public void createAdCampain()
    {
        if (money.GetComponent<MoneyMaking>().getMoney() >= adCampainTotalPrice) {
            if (adBoostType == "money") {
                money.GetComponent<MoneyMaking>().pay(adCampainTotalPrice);
                if (campainDuration == 1.5)
                    money.GetComponent<MoneyMaking>().createAdd(50, 2, adName.text);
                if (campainDuration == 2)
                    money.GetComponent<MoneyMaking>().createAdd(75, 2, adName.text);
                if (campainDuration == 4)
                    money.GetComponent<MoneyMaking>().createAdd(150, 2, adName.text);
            }
            if (adBoostType == "speed") {
                money.GetComponent<MoneyMaking>().pay(adCampainTotalPrice);
                if (campainDuration == 1.5)
                    productionManagement.GetComponent<ProductionManagement>().createAdd(50, (float)0.5, adName.text);
                if (campainDuration == 2)
                    productionManagement.GetComponent<ProductionManagement>().createAdd(75, (float)0.5, adName.text);
                if (campainDuration == 4)
                    productionManagement.GetComponent<ProductionManagement>().createAdd(150, (float)0.5, adName.text);

            }
        }
    }
}
