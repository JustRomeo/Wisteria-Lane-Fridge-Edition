using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int playerID = 0;
    public static string playerColor = "red";

    public static int playerMoney = 100;
    public static int opponentMoney = 100;
    public static int fridgeProduction = 1;
    public static int opponentFridgeProduction = 1;

    public static int carAmount = 1;
    public static int truckAmount = 1;
    public static int carCapacity = 1;
    public static int truckCapacity = 1;
    public static int opponentCarAmount = 1;
    public static int opponentTruckAmount = 1;
    public static int opponentCarCapacity = 1;
    public static int opponentTruckCapacity = 1;

    public static bool playerWin = false;
}
