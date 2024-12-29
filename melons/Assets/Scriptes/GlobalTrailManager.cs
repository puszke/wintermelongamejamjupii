using UnityEngine;

public class GlobalTrailManager : MonoBehaviour
{
    public static GlobalTrailManager instance;  
    public int trailNum = 0;
    public int maxTrailsAvaliable = 0;


    private void Awake()
    {
        instance = this; 
    }

    public void AddTracks(int amount)
    {
        maxTrailsAvaliable += amount;
        trailNum += amount;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
