using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    void Start() {
        print("Connecting to server.");
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.NickName = "Player" + Random.Range(0, 99999).ToString();
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster() {
        print("Connected to server.");
        print(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause) {
        print("Disconnected from the server reason: " + cause.ToString());
    }
}
