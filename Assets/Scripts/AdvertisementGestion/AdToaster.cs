using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdToaster : MonoBehaviour
{
    public float remainingTime;
    public float time;
    public Image adImage;
    public TMP_Text adText;

    //0 = pub, 1 = spy, 2 = sabotage
    private int type;

    public GameObject toasterList;
    // Start is called before the first frame update
    void Start()
    {
        adImage.fillAmount = 0;
        time = 0;
        type = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float fillAmount = time / remainingTime;
        adImage.fillAmount = fillAmount;
        if (time > remainingTime) {
            toasterList.GetComponent<ToasterList>().resetAllToasterPlace(gameObject);
            Destroy(gameObject);
        }
    }

    public void setRemainingTime(float remaining)
    {
        remainingTime = remaining;
    }

    public void setAdTitle(string adTitle)
    {
        adText.text = adTitle;
    }

    public void setType(int toasterType)
    {
        type = toasterType;
    }

    public int getType()
    {
        return (type);
    }

    public void setToasterList(GameObject toasterListObject)
    {
        toasterList = toasterListObject;
    }

    public void resetToaster()
    {
        time = 0;
    }

    public void destroyAndUpdateList()
    {
        toasterList.GetComponent<ToasterList>().resetAllToasterPlace(gameObject);
        Destroy(gameObject);
    }
}
