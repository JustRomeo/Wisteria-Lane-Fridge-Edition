using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public GameObject mainCanvas;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void closeCanvas()
    {
        mainCanvas.transform.localPosition = new Vector3(1500, 0, 0);
    }
}