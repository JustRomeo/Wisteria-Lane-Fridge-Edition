using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductionChainDamage : MonoBehaviour
{
    public Slider budgetManagement;
    private int maintenanceBudget;
    private int maintenancePercent;
    private int fullBudgetMaintenance;

    public GameObject productionGestion;

    public TMP_Text maintenanceBudgetText;
    public TMP_Text maintenancePercentageText;

    int fridgePrice;

    // Start is called before the first frame update
    void Start()
    {
        maintenancePercent = 5;
        productionChainMaintenanceBudgetChange();
        fridgePrice = 175;
    }

    void Update()
    {
        productionChainMaintenanceBudgetChange();
    }

    public void productionChainMaintenanceBudgetChange()
    {
        fridgePrice = productionGestion.GetComponent<ProductionManagement>().getFridgePrice();

        maintenancePercent = (int)budgetManagement.value;
        maintenanceBudget = (int)(budgetManagement.value * 8 * ((int)fridgePrice * 0.025));
        fullBudgetMaintenance = fridgePrice * maintenanceBudget;
        maintenanceBudgetText.text = " = " + maintenanceBudget.ToString();
        maintenancePercentageText.text = "Maintenance percentage: " + ((int)(budgetManagement.value * 10)).ToString() + "%";

        productionGestion.GetComponent<ProductionManagement>().setProdMaintenanceBudget((int)budgetManagement.value);
    }
}
