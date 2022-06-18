using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaintenanceBudget : MonoBehaviour
{
    public Text fullBudgetMaintenanceText;

    // private int budget;
    private int fullBudgetMaintenance;

    public GameObject money;
    // Start is called before the first frame update
    void Start()
    {
        // budget = 150;
        fullBudgetMaintenance = 450;
        fullBudgetMaintenanceText.text = fullBudgetMaintenance.ToString() + "$";
        money.GetComponent<MoneyMaking>().setCarMaintenanceBudget(fullBudgetMaintenance);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void udpdateFullBudget(int fullBudget)
    {
        fullBudgetMaintenance = fullBudget;
        fullBudgetMaintenanceText.text = fullBudgetMaintenance.ToString() + "$";
        money.GetComponent<MoneyMaking>().setCarMaintenanceBudget(fullBudgetMaintenance);
    }
}
