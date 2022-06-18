using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject RoomCanvas;
    [SerializeField]
    private GameObject MainMenuCanvas;

    public void DisplayRoomCanvas() {
        RoomCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }
}
