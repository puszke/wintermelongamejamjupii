using UnityEngine;
using UnityEngine.UI;
public class TracksSlider : MonoBehaviour
{
    Image img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img= GetComponent<Image>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalTrailManager.instance.maxTrailsAvaliable>0)
            img.fillAmount = (float)GlobalTrailManager.instance.trailNum / (float)GlobalTrailManager.instance.maxTrailsAvaliable;
       
    }
}
