using UnityEngine;

public class Stations : MonoBehaviour
{
    public int TrailsAvaliable = 100;
    public float radius = 10;
    private GameObject[] players;
    private bool visited = false;
    private bool used = false;

    public GameObject trainVisitingIt, icon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0)&&!used)
        {
            trainVisitingIt.GetComponent<TrainController>().isRiding = true;
            used = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Visit()
    {
        GlobalTrailManager.instance.AddTracks(TrailsAvaliable);
        visited = true;
    }
    // Update is called once per frame
    void Update()
    {
        icon.SetActive(visited && !used);
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            if (!visited && Vector3.Distance(transform.position, p.transform.position) < radius)
            {
                trainVisitingIt = p;
                Visit();
            }
        }
        
    }
}
