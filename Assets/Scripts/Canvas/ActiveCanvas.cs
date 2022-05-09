using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCanvas : MonoBehaviour
{
    private GameObject openedCanvas;
    private bool isACanvasOpen;
    // Start is called before the first frame update
    void Start()
    {
        openedCanvas = null;
        isACanvasOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void closeCanvas()
    {
        openedCanvas = null;
        isACanvasOpen = false;
    }

    public void setCanvasActive(GameObject activeCanvas)
    {
        openedCanvas = activeCanvas;
        isACanvasOpen = true;
    }

    public bool getActiveCanvas()
    {
        return (isACanvasOpen);
    }
}
