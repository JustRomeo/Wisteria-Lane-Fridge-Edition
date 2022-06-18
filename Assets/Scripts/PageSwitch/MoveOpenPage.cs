using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOpenPage : MonoBehaviour
{

    public GameObject pageToAppear;
    public List<GameObject> pageToDiseappear;

    public GameObject father;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void moveOpenPage()
    {
        pageToAppear.transform.localPosition = new Vector3(0, -136, 0);
        pageToAppear.transform.SetParent(father.transform, false);
        for (int i = 0; i < pageToDiseappear.Count; i++) {
            pageToDiseappear[i].transform.localPosition = new Vector3(2000, -136, 0);
            pageToDiseappear[i].transform.SetParent(father.transform, false);
        }
    }
}
