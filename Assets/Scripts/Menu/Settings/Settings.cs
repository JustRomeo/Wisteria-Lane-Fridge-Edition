using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {
    void Start() {}
    void Update() {}

    public void unload() {SceneManager.UnloadSceneAsync("Assets/Scenes/Menu/Settings.unity");}
}
