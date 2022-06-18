using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RoomListElement : MonoBehaviour
{
    [SerializeField]
    private Text _info;

    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo) {
        RoomInfo = roomInfo;
        _info.text = roomInfo.Name;
    }
}
