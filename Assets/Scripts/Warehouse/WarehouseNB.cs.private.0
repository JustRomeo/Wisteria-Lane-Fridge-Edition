using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WarehouseNB : MonoBehaviour
{
    private int nbMaxPlace;
    private List<int> nbFridgeInWareHouse;

    private Text warehousePlaceText;

    public GameObject fridgeInWarehouseList;
    // Start is called before the first frame update
    void Start()
    {
        nbMaxPlace = 20;
        nbFridgeInWareHouse = new List<int>();
        warehousePlaceText = GetComponent<Text>();
        warehousePlaceText.text = nbFridgeInWareHouse.Count.ToString() + "/" + nbMaxPlace.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void addFridge(int nbFridge, int fridgePrice)
    {
        if (nbFridgeInWareHouse.Count + nbFridge > nbMaxPlace) {
            int nbPlace = nbMaxPlace - nbFridgeInWareHouse.Count;
            for (int i = 0; i < nbPlace; i++) {
                nbFridgeInWareHouse.Add(fridgePrice);
                fridgeInWarehouseList.GetComponent<WarehouseFridgeList>().addFridge(fridgePrice);
            }
        }
        else {
            for (int i = 0; i < nbFridge; i++) {
                fridgeInWarehouseList.GetComponent<WarehouseFridgeList>().addFridge(fridgePrice);
                nbFridgeInWareHouse.Add(fridgePrice);
            }
        }
        warehousePlaceText.text = nbFridgeInWareHouse.Count.ToString() + "/" + nbMaxPlace.ToString();
    }

    public List<int> removeFridge(int nbFridgeToRemove)
    {
        List<int> listToReturn = new List<int>();
        if (nbFridgeToRemove < nbFridgeInWareHouse.Count) {
            for (int i = 0; i < nbFridgeToRemove; i++) {
                listToReturn.Add(nbFridgeInWareHouse[0]);
                nbFridgeInWareHouse.RemoveAt(0);
                fridgeInWarehouseList.GetComponent<WarehouseFridgeList>().removeFridge();
            }
            warehousePlaceText.text = nbFridgeInWareHouse.Count.ToString() + "/" + nbMaxPlace.ToString();
            return (listToReturn);
        }
        else if (nbFridgeToRemove > nbFridgeInWareHouse.Count) {
            for (int i = 0; i < nbFridgeInWareHouse.Count; i++) {
                listToReturn.Add(nbFridgeInWareHouse[0]);
                fridgeInWarehouseList.GetComponent<WarehouseFridgeList>().removeFridge();
            }
            nbFridgeInWareHouse.Clear();
            warehousePlaceText.text = nbFridgeInWareHouse.Count.ToString() + "/" + nbMaxPlace.ToString();
            return (listToReturn);
        }
        return (listToReturn);
    }

    public List<int> getNextExtract(int nbFridgeToRemove)
    {
        List<int> listToReturn = new List<int>();
        if (nbFridgeToRemove < nbFridgeInWareHouse.Count) {
            for (int i = 0; i < nbFridgeToRemove; i++)
                listToReturn.Add(nbFridgeInWareHouse[0]);
            return (listToReturn);
        }
        else if (nbFridgeToRemove > nbFridgeInWareHouse.Count) {
            for (int i = 0; i < nbFridgeInWareHouse.Count; i++)
                listToReturn.Add(nbFridgeInWareHouse[0]);
            return (listToReturn);
        }
        return (listToReturn);
    }
    public void increaseMaxPlace(int addPlace)
    {
        nbMaxPlace += addPlace;
        warehousePlaceText.text = nbFridgeInWareHouse.Count.ToString() + "/" + nbMaxPlace.ToString();
    }
}
