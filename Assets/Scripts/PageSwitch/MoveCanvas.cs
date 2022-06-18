using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCanvas : MonoBehaviour
{
    public AudioClip clip;
    public float volume = 10;

    public GameObject mainCanvas;

    public GameObject canvasInfo;
    public List<GameObject> otherPage;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void moveCanvas()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        mainCanvas.transform.localPosition = new Vector3(0, 0, 0);
        canvasInfo.GetComponent<ActiveCanvas>().setCanvasActive(mainCanvas);
        for (int i = 0; i < otherPage.Count; i++) {
            otherPage[i].transform.localPosition = new Vector3(2000, 0, 0);
        }
    }
}
