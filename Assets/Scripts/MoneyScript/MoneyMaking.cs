using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyMaking : MonoBehaviour
{

    public int money;
    public int moneyPerHour;
    public Text moneyText;

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
        moneyText.text = money.ToString() + "$";
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

    public void pay(int valueToPay)
    {
        money -= valueToPay;
        moneyText.text = money.ToString() + "$";
    }

    public void createAdd(float timeBoost, float moneyBoost, string adName)
    {
        time = 0;
        isAdCampainActive = true;
        adMoneyBoost = moneyBoost;
        adTimeBoost = timeBoost;
        createAdInfoCanvas(adName);
    }

    void createAdInfoCanvas(string adName)
    {
        GameObject newAd = Instantiate(adPrefab, new Vector3(610, 310, 0), Quaternion.identity);
        newAd.transform.SetParent(Canvas.transform, false);
        newAd.GetComponent<AdToaster>().setRemainingTime(adTimeBoost);
        newAd.GetComponent<AdToaster>().setAdName(adName);
    }
}
