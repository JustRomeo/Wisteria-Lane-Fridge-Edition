using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageCanvas : MonoBehaviour
{
    public GameObject unlockedFeatureCanvas;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void unlockSabotage()
    {
        unlockedFeatureCanvas.SetActive(false);
    }
}
