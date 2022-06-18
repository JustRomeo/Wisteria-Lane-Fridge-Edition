using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDamageStat
{
    private float damageMoneyDrop;
    private float damageTimeDuration;
    private int damageRepairCost;
    private int damageType;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void setData(float moneyDrop, float timeDuration, int type, int repairCost)
    {
        damageMoneyDrop = moneyDrop;
        damageTimeDuration = timeDuration;
        damageRepairCost = repairCost;
        damageType = type;
    }

    public float getMoneyDrop()
    {
        return (damageMoneyDrop);
    }

    public float getTimeDuration()
    {
        return (damageTimeDuration);
    }

    public int getRepairCost()
    {
        return (damageRepairCost);
    }

    public int getDamageType()
    {
        return (damageType);
    }
}
