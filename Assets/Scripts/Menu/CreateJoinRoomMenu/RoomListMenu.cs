using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListElement _roomListElem;
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private CreateRoomMenu _createRoomMenu;

    private List<RoomListElement> _roomListManager = new List<RoomListElement>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        foreach (RoomInfo info in roomList) {
            if (info.RemovedFromList) {
                removeRoomFromList(info);
            } else {
                if (_roomListManager.FindIndex(x => x.RoomInfo.Name == info.Name) != -1) {
                    if (info.PlayerCount == 2) {
                        removeRoomFromList(info);
                    }
                } else {
                    RoomListElement elem = Instantiate(_roomListElem, _content);
                    if (elem != null) {
                        elem.SetRoomInfo(info);
                        _roomListManager.Add(elem);
                        Button button = elem.GetComponent<Button>();
                        button.onClick.AddListener(delegate { _createRoomMenu.OnClick_JoinRoom(info.Name); });
                    }
                }
            }
        }
    }

    public void removeRoomFromList(RoomInfo info) {
        int index = _roomListManager.FindIndex(x => x.RoomInfo.Name == info.Name);
        if (index != -1) {
            Destroy(_roomListManager[index].gameObject);
            _roomListManager.RemoveAt(index);
        }
    }
}
