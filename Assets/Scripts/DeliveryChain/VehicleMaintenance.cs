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
    public TMP_Text maintenancePercentageText;
    public GameObject fullBudgetMaintenanceObject;

    // Start is called before the first frame update
    void Start()
    {
        maintenancePercent = 5;
        vehicleMaintenanceBudgetChange();
    }

    void Update()
    {
        vehicleMaintenanceBudgetChange();
    }

    public void vehicleMaintenanceBudgetChange()
    {
        int nbOfCar = deliveryChain.GetComponent<DeliveryChain>().getNbOfCar();
        int nbOfTruck = deliveryChain.GetComponent<DeliveryChain>().getNbOfTruck();

        maintenancePercent = (int)budgetManagement.value;
        maintenanceBudget = (int)(budgetManagement.value * 35);
        fullBudgetMaintenance = nbOfCar * maintenanceBudget + nbOfTruck * maintenanceBudget * 2;
        maintenanceBudgetText.text = " = " + maintenanceBudget.ToString();
        maintenancePercentageText.text = "Maintenance percentage: " + ((int)(budgetManagement.value * 10)).ToString() + "%";

        fullBudgetMaintenanceObject.GetComponent<MaintenanceBudget>().udpdateFullBudget(fullBudgetMaintenance);
        deliveryChain.GetComponent<DeliveryChain>().setMaintenancePercent((int)budgetManagement.value);
    }
}
