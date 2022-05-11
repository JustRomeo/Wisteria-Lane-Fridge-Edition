using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class VehicleMaintenance : MonoBehaviour
{
    public Slider budgetManagement;
    private int maintenanceBudget;
    private int maintenancePercent;
    private int fullBudgetMaintenance;

    public GameObject deliveryChain;

    public TMP_Text maintenanceBudgetText;
    public GameObject fullBudgetMaintenanceObject;

    // Start is called before the first frame update
    void Start()
    {
        maintenancePercent = 3;
    }

    // Update is called once per frame
    // void Update()
    // {
    // }

    public void vehicleMaintenanceBudgetChange()
    {
        int nbOfCar = deliveryChain.GetComponent<DeliveryChain>().getNbOfCar();
        int nbOfTruck = deliveryChain.GetComponent<DeliveryChain>().getNbOfTruck();

        maintenancePercent = (int)budgetManagement.value;
        maintenanceBudget = (int)(budgetManagement.value * 50);
        fullBudgetMaintenance = nbOfCar * maintenanceBudget + nbOfTruck * maintenanceBudget * 2;
        maintenanceBudgetText.text = " = " + maintenanceBudget.ToString();

        fullBudgetMaintenanceObject.GetComponent<MaintenanceBudget>().udpdateFullBudget(fullBudgetMaintenance);
        deliveryChain.GetComponent<DeliveryChain>().setMaintenancePercent((int)budgetManagement.value);
    }
}
