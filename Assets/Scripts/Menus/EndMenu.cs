using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using TMPro;

public class EndMenu : MonoBehaviour {

    private TextMeshPro scoretext;

    void Start() {
        scoretext = gameObject.GetComponent<TextMeshPro>();
    }
    void Update() {
        int playerprefvalues = PlayerPrefs.GetInt("enterpriseValue");
        string newstring = "Your enterprise value is:\n\n" + playerprefvalues.ToString() + "$";

        if (scoretext)
            print("Text: Ok");
        else
            print("Text: Null");

        // scoretext.SetText(newstring);
        // scoretext.text = newstring;
    }
}
