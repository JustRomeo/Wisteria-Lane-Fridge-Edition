using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPageManagement : MonoBehaviour
{
    public List<GameObject> pages;

    private int position;

    public GameObject fowardButton;
    public GameObject backwardButton;
    // Start is called before the first frame update
    void Start()
    {
        position = 0;
        pages[0].SetActive(true);
        for (int i = 1; i < pages.Count; i++) {
            pages[i].SetActive(false);
        }
    }

    // Update is called once per frame
    public void moveBackward()
    {
        if (position > 0) {
            backwardButton.SetActive(true);
            fowardButton.SetActive(true);
            position--;
            pages[position].SetActive(true);
            for (int i = 0; i < pages.Count; i++) {
                if (i != position)
                    pages[i].SetActive(false);
            }
        }
        if (position == 0)
            backwardButton.SetActive(false);
    }
    public void moveForward()
    {
        if (position < pages.Count - 1) {
            backwardButton.SetActive(true);
            fowardButton.SetActive(true);
            position++;
            pages[position].SetActive(true);
            for (int i = 0; i < pages.Count; i++) {
                if (i != position)
                    pages[i].SetActive(false);
            }
        }
        if (position == pages.Count - 1)
            fowardButton.SetActive(false);
    }
}
