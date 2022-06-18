using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ReplayButton : MonoBehaviour
{
    public void OnClickReplay() {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Scenes/Launcher");
    }
}
