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

    private float adTimeBoost;
    private float adMoneyBoost;
    private bool isAdCampainActive;

    public GameObject adPrefab;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        isAdCampainActive = false;
        adMoneyBoost = 1;
        adTimeBoost = 1;
        money = 5000;
        moneyPerHour = 750;
        moneyText.text = money.ToString() + "$";
        moneyPerHourText.text = moneyPerHour.ToString() + "$/h";
        time = 0;

        enterpriseValue = money;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("enterpriseValue", enterpriseValue);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void earnMoney(int moneyEarned)
    {
        time += Time.deltaTime;
        if (isAdCampainActive == true && time < adTimeBoost) {
            moneyEarned = (int)(moneyEarned * adMoneyBoost);
        }
        if (time > adTimeBoost) {
            isAdCampainActive = false;
            time = 0;
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
        if (isAdCampainActive == true && time < adTimeBoost) {
            boost = adMoneyBoost;
        }
        if (time > adTimeBoost) {
            isAdCampainActive = false;
            time = 0;
        }
        nextMoneyPerHour = (int)(newMoneyPerHour * boost);
        moneyPerHourText.text = nextMoneyPerHour.ToString() + "$/h";
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

    public void createAdd(float timeBoost, float moneyBoost, string adName, string adType)
    {
        time = 0;
        isAdCampainActive = true;
        adMoneyBoost = moneyBoost;
        adTimeBoost = timeBoost;
        createAdInfoCanvas(adName, adType);
    }

    void createAdInfoCanvas(string adName, string adType)
    {
        GameObject newAd = Instantiate(adPrefab, new Vector3(610, 310, 0), Quaternion.identity);
        newAd.transform.SetParent(Canvas.transform, false);
        newAd.GetComponent<AdToaster>().setRemainingTime(adTimeBoost);
        newAd.GetComponent<AdToaster>().setAdTitle(adName, adType);
    }
}
