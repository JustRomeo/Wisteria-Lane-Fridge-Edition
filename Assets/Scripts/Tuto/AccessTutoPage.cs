using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessTutoPage : MonoBehaviour
{
    public GameObject tutoPage;

    public GameObject canvasInfo;

    public List<GameObject> gamePages;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void openTutoPage()
    {
        tutoPage.transform.localPosition = new Vector3(0, 0, 0);
        canvasInfo.GetComponent<ActiveCanvas>().setCanvasActive(tutoPage);
        for (int i = 0; i < gamePages.Count; i++) {
            gamePages[i].transform.localPosition = new Vector3(1500, 0, 0);
        }
    }
}
