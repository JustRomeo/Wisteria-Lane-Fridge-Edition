using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Server : MonoBehaviourPunCallbacks
{
    /*[SerializeField]
    private List<CubeManager> cubes;*/

    void Awake() {
        if (PhotonNetwork.IsMasterClient) {
            /*ExitGames.Client.Photon.Hashtable propreties = PhotonNetwork.CurrentRoom.CustomProperties;

            foreach(var cube in cubes) {
                propreties.Add("cube" + cube.view.ViewID, "white");
            }

            Dictionary<int, Player> players = PhotonNetwork.CurrentRoom.Players;
            foreach(var player in players) {
                if (player.Value.NickName == PhotonNetwork.NickName) {
                    propreties.Add(PhotonNetwork.NickName + "Color", "red");*/
                    PlayerManager.playerID = 0;
                    PlayerManager.playerColor = "red";
                    print("PLAYER BLUE");
                /*}
                if (player.Value.NickName != PhotonNetwork.NickName)
                    propreties.Add(player.Value.NickName + "Color", "blue");
            }*/

            //PhotonNetwork.CurrentRoom.SetCustomProperties(propreties);
        } else {
            PlayerManager.playerID = 1;
            PlayerManager.playerColor = "blue";
            print("PLAYER RED");
            //ExitGames.Client.Photon.Hashtable propreties = PhotonNetwork.CurrentRoom.CustomProperties;

            //PlayerManager.playerColor = (string)propreties[PhotonNetwork.NickName + "Color"];
        }
    }
}
