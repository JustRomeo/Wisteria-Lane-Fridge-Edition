using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class PlayerName : MonoBehaviour
{

    public TMP_Text playerName;
    // Start is called before the first frame update
    void Start()
    {
        playerName.text = PhotonNetwork.NickName;
    }
}
