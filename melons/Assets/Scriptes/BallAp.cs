using System.Collections;
using UnityEngine;

public class BallAp : MonoBehaviour
{


    IEnumerator balls()
    {
        transform.tag = "Untagged";
        gameObject.layer = 0;
        GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        transform.tag = "Path";
        gameObject.layer = 11;
        GetComponent<SphereCollider>().enabled = true;

    }


    void Start()
    {
        StartCoroutine(balls());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
