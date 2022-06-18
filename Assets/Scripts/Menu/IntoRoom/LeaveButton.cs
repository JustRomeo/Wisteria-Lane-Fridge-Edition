using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LeaveButton : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject createAndJoinRoomCanvas;
    [SerializeField]
    private GameObject intoRoomCanvas;

    public void OnClickLeave() {
        createAndJoinRoomCanvas.SetActive(true);
        intoRoomCanvas.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }

    // public override void OnLeftRoom() {
    //     print("Room left");
    // }
}
