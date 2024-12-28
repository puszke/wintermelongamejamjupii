using System.Collections;
using UnityEngine;

public class BallAp : MonoBehaviour
{

    public bool startPoint=true,endPoint = true;

    public LayerMask pointMask;

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
        if (startPoint)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (endPoint)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 4, pointMask))
        {
            endPoint = hit.transform == null;
            Debug.Log(transform.name+ " " + hit.transform.root.name);
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 4, pointMask))
        {
            startPoint = hit.transform == null;
        }
    }
}
