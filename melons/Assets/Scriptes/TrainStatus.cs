using UnityEngine;

public class TrainStatus : MonoBehaviour
{

    public int temperature = 0;
    public int electricCharge = 0;

    public float influenceSphere = 5f; //z jak wielkiej odleg�o�ci wp�ywa poci�g na inne poci�gi :3
    

    //Nie wiem jak zrobi� aby wykrywa� inne poci�gi na wi�ksz� odleg�o��, ale nie nas samych >///<, ale to nie jest a� tak potrzebny feature :3
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
