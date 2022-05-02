using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FridgeInWarehouse : MonoBehaviour
{
    public Text fridgePriceText;

    private int fridgePrice;
    // Start is called before the first frame update
    void Awake()
    {
        fridgePrice = 0;
        fridgePriceText.text = fridgePrice.ToString() + "$";
    }

    public void setFridgePrice(int price)
    {
        fridgePrice = price;
        fridgePriceText.text = fridgePrice.ToString() + "$";
    }
}
