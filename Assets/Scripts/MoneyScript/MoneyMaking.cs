using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMaking : MonoBehaviour
{

    public int money;
    public int moneyPerHour;
    public Text moneyText;

    private int nextMoneyPerHour;
    public Text moneyPerHourText;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        money = 5000;
        moneyPerHour = 750;
        moneyText.text = money.ToString() + "$";
        moneyPerHourText.text = moneyPerHour.ToString() + "$/h";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void earnMoney(int moneyEarned)
    {
        money += moneyEarned;
        moneyText.text = money.ToString() + "$";
    }

    public void increaseMoneyEarning(int newMoneyPerHour)
    {
        nextMoneyPerHour = newMoneyPerHour;
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
}
