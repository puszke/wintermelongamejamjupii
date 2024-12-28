using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackShow : MonoBehaviour
{
    public GameObject[] points;

    public GameObject trail;

    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CheckTime());
    }

    IEnumerator CheckTime()
    {
        yield return new WaitForSeconds(0.1f);
        
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("TRAIL"))
        {
            Destroy(b);
        }

        points = GameObject.FindGameObjectsWithTag("Path");

        for (int i = 0; i < points.Length; i++)
        {
            if (i != points.Length - 1)
            {
                if (Vector3.Distance(points[i].transform.position, points[i + 1].transform.position) < 5)
                {
                    GameObject newTrails = Instantiate(trail);
                    LineRenderer line = newTrails.GetComponent<LineRenderer>();
                    Vector3[] nPositions = new Vector3[2];
                    nPositions[0] = points[i].transform.position + offset;
                    nPositions[1] = points[i + 1].transform.position + offset;
                    line.SetPositions(nPositions);
                }
            }
        }
        StartCoroutine(CheckTime());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
