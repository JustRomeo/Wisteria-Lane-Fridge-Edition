using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartGameButton : MonoBehaviour
{
    public void OnClickStart() {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.Players.Count == 2) {
            PhotonNetwork.LoadLevel(1);
        }
    }
}
