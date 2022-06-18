using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

    [SerializeField]
    private Text _playerName;

    public GameObject _intoRoomCanvas;
    public GameObject _createAndJoinRoomCanvas;

    private IntoRoomMenu _intoRoomMenu;

    public void Awake() {
        _intoRoomMenu = _intoRoomCanvas.GetComponent<IntoRoomMenu>();
    }

    public void OnClick_CreateRoom() {
        if (!PhotonNetwork.IsConnected)
            return;
        if (_roomName.text == "")
            return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        if (_playerName.text != "")
            PhotonNetwork.NickName = _playerName.text;

        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom() {
        print("Created room sucessfully.");
        displayIntoRoomCanvas();
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        print("Created room failed: " + message);
    }

    public void OnClick_JoinRoom(string roomName) {
        if (_playerName.text != "")
            PhotonNetwork.NickName = _playerName.text;
        PhotonNetwork.JoinRoom(roomName);
    }
    
    public override void OnJoinedRoom() {
        print("Joined room sucessfully.");
        displayIntoRoomCanvas();
    }

    public void displayIntoRoomCanvas() {
        _intoRoomCanvas.SetActive(true);
        _createAndJoinRoomCanvas.SetActive(false);
        _intoRoomMenu.SetRoomInfo();
    }
}
