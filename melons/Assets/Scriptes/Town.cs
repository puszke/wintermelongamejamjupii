using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Town : MonoBehaviour
{
    public string COLOR = "RED";
    public bool isTrainIn = false;
    GameObject[] players;
    
    bool visited = false;
    public float radius = 10;

    GameObject trainVisitingIt;
    public GameObject icon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void Visit()
    {
        isTrainIn = true;
        Destroy(trainVisitingIt);
    }
    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        icon.SetActive(!isTrainIn);
        foreach (GameObject p in players)
        {
            if (!visited && Vector3.Distance(transform.position, p.transform.position) < radius)
            {
                if (p.GetComponent<TrainController>().COLOR == COLOR)
                {
                    trainVisitingIt = p;
                    Visit();
                }
            }
        }
    }
}
