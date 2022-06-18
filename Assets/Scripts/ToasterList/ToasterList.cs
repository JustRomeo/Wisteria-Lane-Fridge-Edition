using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterList : MonoBehaviour
{
    private List<GameObject> toasterList;
    // Start is called before the first frame update
    void Start()
    {
        toasterList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void addToaster(GameObject newToaster)
    {
        Vector3 lastPosition;

        if (toasterList.Count > 0) {
            lastPosition = toasterList[toasterList.Count - 1].transform.localPosition;
        }
        else {
            lastPosition = new Vector3(-35, 420, 0);
        }
        newToaster.transform.localPosition = new Vector3(lastPosition.x, lastPosition.y - 120, lastPosition.z);
        newToaster.transform.SetParent(this.transform, false);
        toasterList.Add(newToaster);
    }

    public void resetAllToasterPlace(GameObject toasterToRemove)
    {
        Vector3 initPos = new Vector3(-35, 420, 0);
        int posToRemove = 0;

        for (int i = 0; i < toasterList.Count; i++) {
            if (toasterList[i] == toasterToRemove)
                posToRemove = i;
        }
        toasterList.RemoveAt(posToRemove);
        for (int i = 0; i < toasterList.Count; i++) {
            toasterList[i].transform.localPosition = new Vector3(initPos.x, initPos.y - 120, initPos.z);
            initPos = toasterList[i].transform.localPosition;
        }
    }

    public void resetAdToaster(int toasterType)
    {
        for (int i = 0; i < toasterList.Count; i++) {
            if (toasterList[i].GetComponent<AdToaster>().getType() == toasterType)
                toasterList[i].GetComponent<AdToaster>().resetToaster();
        }
    }
}
