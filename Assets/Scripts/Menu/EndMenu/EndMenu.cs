using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using TMPro;

public class EndMenu : MonoBehaviour {
    public Text TxtStrength;
    [SerializeField]
    private GameObject winBackground;
    [SerializeField]
    private GameObject loseBackground;

    void Start() {
        print("PlayerManager.playerWin");
        if (PlayerManager.playerWin == true) {
            winBackground.SetActive(true);
        } else {
            loseBackground.SetActive(true);
        }

        TxtStrength = GetComponent<Text>();
        TxtStrength.text = "Your enterprise value is:\n\n" + PlayerPrefs.GetInt("enterpriseValue").ToString() + "$";
    }
}
