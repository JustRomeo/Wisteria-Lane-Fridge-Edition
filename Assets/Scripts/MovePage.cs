using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePage : MonoBehaviour
{

    public GameObject pageToAppear;
    public List<GameObject> pageToDiseappear;
    // Start is called before the first frame update

    public void movePage()
    {
        pageToAppear.SetActive(true);
        for (int i = 0; i < pageToDiseappear.Count; i++) {
            pageToDiseappear[i].SetActive(false);
        }
    }
}
