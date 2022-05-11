using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public GameObject mainCanvas;

    public GameObject canvasInfo;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void closeCanvas()
    {
        canvasInfo.GetComponent<ActiveCanvas>().closeCanvas();
        mainCanvas.transform.localPosition = new Vector3(2000, 0, 0);
    }
}
