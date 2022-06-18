using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToStart : MonoBehaviour
{
    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private GameObject quitButton;
    [SerializeField]
    private GameObject settingsButton;
    [SerializeField]
    private GameObject pressToStartButton;
    [SerializeField]
    private Animator cameraAnimator;
    [SerializeField]
    private Animator fridgeAnimator;

    public void PressToStart() {
        playButton.SetActive(true);
        quitButton.SetActive(true);
        settingsButton.SetActive(true);
        pressToStartButton.SetActive(false);
        fridgeAnimator.enabled = true;
        cameraAnimator.enabled = true;
    }
}
