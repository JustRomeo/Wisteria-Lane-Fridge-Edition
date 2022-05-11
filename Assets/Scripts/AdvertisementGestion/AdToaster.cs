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
    // Start is called before the first frame update
    void Start()
    {
        // adImage = GetComponentsInChildren<Image>()[0];
        // adText = GetComponentsInChildren<TMP_Text>()[0];
        // adText.text = "new ad launched";
        adImage.fillAmount = 0;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float fillAmount = time / remainingTime;
        adImage.fillAmount = fillAmount;
        if (time > remainingTime) {
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
}
