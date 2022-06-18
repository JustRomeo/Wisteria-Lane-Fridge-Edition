using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CubeManager : MonoBehaviourPunCallbacks
{
    public PhotonView view;
    public string cubeColor = "white";

    void Start() {
        view = GetComponent<PhotonView>();
    }

    void OnMouseDown() {
        ChangeColor();
    }

    public void ChangeColor() {
        view.RPC("ChangeColorRPC", RpcTarget.All, PlayerManager.playerColor);
    }

    [PunRPC]
    void ChangeColorRPC(string color) {
        if (cubeColor == "white") {
            cubeColor = color;
            if (color == "red")
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            else if (color == "blue")
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
