using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class IntoRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text playerName;
    [SerializeField]
    private Text opponentPlayerName;
    [SerializeField]
    private Text roomName;

    public void SetRoomInfo() {
        playerName.text = PhotonNetwork.NickName;
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        Dictionary<int, Player> players = PhotonNetwork.CurrentRoom.Players;
        foreach(var player in players) {
            if (player.Value.NickName != playerName.text)
                opponentPlayerName.text = player.Value.NickName;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        opponentPlayerName.text = newPlayer.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        opponentPlayerName.text = "Waiting...";
    }
}
