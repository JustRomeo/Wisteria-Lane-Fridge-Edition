using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class EndCondition : MonoBehaviour {
    // private float gametime = 60;
    // private float gametime = 5; // 5seconds game for demo
    // private float gametime = 1800; // around 30min for a real game
    private float gametime = 300; // 1min game for test and developpment

    private float starttime = 0;

    private float imageTime = 0;

    public Image timeLeft;

    public PhotonView view;

    void Start() {
        starttime = Time.realtimeSinceStartup;
        view = GetComponent<PhotonView>();
    }
    void Update() {
        imageTime += Time.deltaTime;

        float fill = imageTime / gametime;

        timeLeft.fillAmount = fill;
        if (Time.realtimeSinceStartup - starttime > gametime) {
            if (PhotonNetwork.IsMasterClient) {
                CheckWinnerRequest();
            }
        }
    }

    public void CheckWinnerRequest() {
        view.RPC("CheckWinnerRequestRPC", RpcTarget.All, PlayerPrefs.GetInt("enterpriseValue"), PlayerManager.playerID);
    }

    [PunRPC]
    void CheckWinnerRequestRPC(int entrepriseValue, int playerid) {
        if (playerid != PlayerManager.playerID) {
            if (entrepriseValue < PlayerPrefs.GetInt("enterpriseValue")) {
                PlayerManager.playerWin = true;
                view.RPC("PlayerLoseRPC", RpcTarget.All, PlayerManager.playerID);
            } else {
                PlayerManager.playerWin = false;
                view.RPC("PlayerWinRPC", RpcTarget.All, PlayerManager.playerID);
            }
        }
    }

    [PunRPC]
    void PlayerWinRPC(int playerid) {
        if (playerid != PlayerManager.playerID) {
            PlayerManager.playerWin = true;
        }
        SceneManager.LoadScene("Assets/Scenes/Menu/EndScreen.unity", LoadSceneMode.Single);
    }

    [PunRPC]
    void PlayerLoseRPC(int playerid) {
        if (playerid != PlayerManager.playerID) {
            PlayerManager.playerWin = false;
        }
        SceneManager.LoadScene("Assets/Scenes/Menu/EndScreen.unity", LoadSceneMode.Single);
    }
}
