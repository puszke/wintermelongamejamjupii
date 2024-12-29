using UnityEngine;
using TMPro;
public class TrailNumberShowText : MonoBehaviour
{
    TMP_Text txt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        txt = GetComponent<TMP_Text>();    
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = GlobalTrailManager.instance.trailNum.ToString()+"/"+GlobalTrailManager.instance.maxTrailsAvaliable;
    }
}
