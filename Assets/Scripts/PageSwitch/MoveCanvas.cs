using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCanvas : MonoBehaviour
{
    public GameObject mainCanvas;
    public List<GameObject> otherPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void moveCanvas()
    {
        mainCanvas.transform.localPosition = new Vector3(0, 0, 0);
        for (int i = 0; i < otherPage.Count; i++) {
            otherPage[i].transform.localPosition = new Vector3(1500, 0, 0);
        }
    }
}
