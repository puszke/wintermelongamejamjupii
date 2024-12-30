using System;
using UnityEngine;

public class TrainStatus : MonoBehaviour
{

    public int temperature = 0;
    public int electricCharge = 0;

    public float influenceSphere = 5f; //z jak wielkiej odleg�o�ci wp�ywa poci�g na inne poci�gi :3

    private Material defaultMat;
    public Material iceMat;
    public Material fireMat;

    public MeshRenderer train;
    Material[] matArray;

    //Nie wiem jak zrobi� aby wykrywa� inne poci�gi na wi�ksz� odleg�o��, ale nie nas samych >///<, ale to nie jest a� tak potrzebny feature :3
    void Start()
    {
        //train = GetComponent<MeshRenderer>();
        matArray = train.materials;
        defaultMat = matArray[1];
    }

    // Update is called once per frame
    void Update()
    {  
        if (temperature>0)
        {
            matArray[1] = fireMat;
        }
        else if(temperature<0)
        {
            matArray[1] = iceMat;
        }
        else
        {
            matArray[1] = defaultMat;
        }

        train.materials = matArray;
    }

    void twojamama()
    {
        GetComponent<TrainController>().Derail();
    }

}
