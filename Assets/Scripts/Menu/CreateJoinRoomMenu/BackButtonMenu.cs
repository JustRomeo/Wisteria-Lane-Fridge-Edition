using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuCanvas;
    [SerializeField]
    private GameObject createAndJoinRoomCanvas;

    public void OnClickBack() {
        mainMenuCanvas.SetActive(true);
        createAndJoinRoomCanvas.SetActive(false);
    }
}
