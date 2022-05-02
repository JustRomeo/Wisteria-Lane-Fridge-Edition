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
    public Slider adBudgetSlider;
    public TMP_Text adBudgetText;

    private float campainDuration;

    private int adCampainTotalPrice;
    public TMP_Text adCampainTotalPriceText;

    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;

    public GameObject money;

    void Start()
    {
        adBudget = 250;
        campainDuration = 1.5F;
        EventSystem.current.SetSelectedGameObject(Button1);
        Button1.GetComponent<Image>().color = Color.red;
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

    public void createAdCampain()
    {
        if (money.GetComponent<MoneyMaking>().getMoney() >= adCampainTotalPrice) {
            money.GetComponent<MoneyMaking>().pay(adCampainTotalPrice);
            if (campainDuration == 1.5)
                money.GetComponent<MoneyMaking>().createAdd(50, 2, adName.text);
            if (campainDuration == 2)
                money.GetComponent<MoneyMaking>().createAdd(70, 2, adName.text);
            if (campainDuration == 4)
                money.GetComponent<MoneyMaking>().createAdd(150, 2, adName.text);
        }
    }
}
