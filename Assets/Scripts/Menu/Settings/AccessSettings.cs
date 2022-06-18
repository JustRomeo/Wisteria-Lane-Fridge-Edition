using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AccessSettings : MonoBehaviour {
    void Start() {}
    void Update() {}

    public void accesssettings() {
        SceneManager.LoadScene("Assets/Scenes/Menu/Settings.unity", LoadSceneMode.Additive);
        // Application.LoadLevel("Assets/Scenes/Menu/Settings.unity");
    }
}
