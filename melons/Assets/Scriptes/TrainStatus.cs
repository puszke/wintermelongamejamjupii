using UnityEngine;

public class TrainStatus : MonoBehaviour
{

    public int temperature = 0;
    public int electricCharge = 0;

    public float influenceSphere = 5f; //z jak wielkiej odleg³oœci wp³ywa poci¹g na inne poci¹gi :3
    

    //Nie wiem jak zrobiæ aby wykrywaæ inne poci¹gi na wiêksz¹ odleg³oœæ, ale nie nas samych >///<, ale to nie jest a¿ tak potrzebny feature :3
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void twojamama()
    {
        GetComponent<TrainController>().Derail();
    }

}
