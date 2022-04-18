using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseFridgeList : MonoBehaviour
{
    public List<GameObject> fridgeList;

    public GameObject fridgePrefabs;

    public RectTransform fridgeListRect;
    // Start is called before the first frame update
    void Start()
    {
        fridgeList = new List<GameObject>();
        fridgeListRect = GetComponent<RectTransform>();
    }

    public void addFridge(int price)
    {
        Vector3 lastPosition;
        if (fridgeList.Count > 0) {
            Vector2 oldSize = fridgeListRect.sizeDelta;
            fridgeListRect.sizeDelta = new Vector2(oldSize.x, oldSize.y + 75);
            lastPosition = fridgeList[fridgeList.Count - 1].transform.localPosition;
        }
        else {
            lastPosition = new Vector3(0, 75, 0);
        }
        GameObject newFridge = Instantiate(fridgePrefabs, new Vector3(-240, lastPosition.y - 75, 0), Quaternion.identity);
        newFridge.transform.SetParent(this.transform, false);
        fridgeList.Add(newFridge);
    }

    public void removeFridge()
    {
        Vector2 oldSize;

        if (fridgeList.Count > 1) {
            Destroy(fridgeList[0]);
            fridgeList.RemoveAt(0);
            for (int i = 0; i < fridgeList.Count; i++) {
                Vector3 oldPosition = fridgeList[i].transform.localPosition;
                fridgeList[i].transform.localPosition = new Vector3(oldPosition.x, oldPosition.y + 75, oldPosition.z);
            }
            oldSize = fridgeListRect.sizeDelta;
            fridgeListRect.sizeDelta = new Vector2(oldSize.x, oldSize.y - 75);
        }
        else {
            Destroy(fridgeList[0]);
            fridgeList.RemoveAt(0);
            oldSize = fridgeListRect.sizeDelta;
            fridgeListRect.sizeDelta = new Vector2(oldSize.x, 50);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
