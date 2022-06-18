using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyCanvas : MonoBehaviour
{
    public GameObject unlockedFeatureCanvas;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void unlockSpy()
    {
        unlockedFeatureCanvas.SetActive(false);
    }
}
