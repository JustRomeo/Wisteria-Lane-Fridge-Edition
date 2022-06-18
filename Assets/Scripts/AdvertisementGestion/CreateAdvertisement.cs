using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CreateAdvertisement : MonoBehaviour
{
    public GameObject unlockedFeatureCanvas;

    public TMP_InputField adName;

    private int adBudget;
    private int successPercent;
    private string adBoostType;
    public Slider adBudgetSlider;
    public TMP_Text adBudgetText;
    public TMP_Text successPercentText;

    private float campainDuration;
    private float timeCampainDuration;

    private int adCampainTotalPrice;
    public TMP_Text adCampainTotalPriceText;

    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;

    public GameObject MoneyBoostType;
    public GameObject SpeedBoostType;

    public Button CreateAdButton;

    public GameObject money;
    public GameObject productionManagement;

    void Start()
    {
        adBudget = 250;
        campainDuration = 1.5F;
        timeCampainDuration = 50f;
        Button1.GetComponent<Image>().color = Color.red;
        MoneyBoostType.GetComponent<Image>().color = Color.red;
        EventSystem.current.SetSelectedGameObject(Button1);
        EventSystem.current.SetSelectedGameObject(MoneyBoostType);
        adBoostType = "money";
        successPercent = 10;
        successPercentText.text = "Success percent: " + successPercent.ToString() + "$";
        updateTotalPrice();
    }

    public void DaysDuration(float multiplier)
    {
        campainDuration = multiplier;
        if (multiplier == 1.5) {
            Button1.GetComponent<Image>().color = Color.red;
            Button2.GetComponent<Image>().color = Color.white;
            Button3.GetComponent<Image>().color = Color.white;
            timeCampainDuration = 50;
        }
        if (multiplier == 2) {
            Button1.GetComponent<Image>().color = Color.white;
            Button2.GetComponent<Image>().color = Color.red;
            Button3.GetComponent<Image>().color = Color.white;
            timeCampainDuration = 75;
        }
        if (multiplier == 4) {
            Button1.GetComponent<Image>().color = Color.white;
            Button2.GetComponent<Image>().color = Color.white;
            Button3.GetComponent<Image>().color = Color.red;
            timeCampainDuration = 150;
        }
        updateTotalPrice();
    }

    public void changeCampainBudget()
    {
        adBudget = (int)(adBudgetSlider.value * 175);
        adBudgetText.text = adBudget.ToString() + "$";
        successPercent = 10 + (int)((adBudgetSlider.value - 2) * 6);
        successPercentText.text = "Success percent: " + successPercent.ToString() + "%";
        updateTotalPrice();
    }

    void updateTotalPrice()
    {
        adCampainTotalPrice = (int)(adBudget * campainDuration);
        adCampainTotalPriceText.text = "Ad creation total price: " + (adBudget * campainDuration).ToString() + "$";
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
            int randomSuccess = Random.Range(1, 100);
            Debug.Log(randomSuccess.ToString() + " != " + successPercent.ToString());
            if (randomSuccess < successPercent) {
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
                CreateAdButton.interactable = false;
                StartCoroutine(resetAdButton(timeCampainDuration));
            }
            else {
                money.GetComponent<MoneyMaking>().failedAdCreation(10);
                CreateAdButton.interactable = false;
                StartCoroutine(resetAdButton(5));
            }
        }
    }

    IEnumerator resetAdButton(float adTimeDuration)
    {
        yield return new WaitForSeconds(adTimeDuration);
        CreateAdButton.interactable = true;
    }

    public void unlockAdvertisement()
    {
        unlockedFeatureCanvas.SetActive(false);
    }
}
