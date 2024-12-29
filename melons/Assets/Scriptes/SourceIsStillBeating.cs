using System;
using System.Collections;
using UnityEngine;

public class SourceIsStillBeating : MonoBehaviour
{
    public enum SourceType
    {
        HeatSource,
        ColdSource,
        NegativePowerSource,
        PositivePowerSource,
        ColdObstacle,
        HeatObstacle,
        PowerReceiver
    }

    [SerializeField]
    public SourceType WhatIsIt = SourceType.HeatSource; // M�wi nam, czym jest ta rzecz kt�ra ma ten skrypt (jeden skrypt do wszystkiego >:3

    private Action<GameObject> objectAction; // Akcja kt�r� b�dzie wykonywa� nasz obiekt >///<

    public float detectionRadius = 6f;
    bool cooldown = false;
    public float delay = 6f;

    public bool isAStation = false;
    public Station station = null;


    //public bool destructable = true; // Bardzo konkretna zmienna, tyczy si� tylko przeszk�d --- jednak nie, zostawiam ten pomys� na p�niej x3

    private int myTemperature = 0;
    private int myElectricCharge = 0;




    // Update is called once per frame
    void Update()
    {
        DetectTrain();
    }

    private void DetectTrain()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {

            if (Vector3.Distance(transform.position, player.transform.position) < detectionRadius)
            {

                //Tutaj dochodzi do interakcji, je�eli poci�g zbli�y si� do obiektu
                if (!cooldown)
                {
                    objectAction?.Invoke(player);
                    cooldown = true;
                    StartCoroutine(BallsFour(delay));
                }
                break;
            }
        }
    }

    private void DetectStation()
    {
        objectAction?.Invoke(station.trainAtStation);
    }
    



    //Ta metoda jest odpowiedzialna za usuwanie cooldownu bo jakim� czasie :3 - aby zn�w mo�na by�o u�ywa� z tej zmiennej :3
    IEnumerator BallsFour(float time)
    {

        yield return new WaitForSeconds(time);
        cooldown = false;

    }




    //Metody, Ziomek robi tylko t�, kt�r� ma przypisan� do swojego zawodu. Mo�e mie� przypisan� tylko jedn� metod�, przypisywana jest ona tylko raz na starcie.

    void HeatSourceAction(GameObject train)
    {
        train.GetComponent<TrainStatus>().temperature++;
        if (train.GetComponent<TrainStatus>().temperature > 1)
        {
            //mo�na najpierw da� jak�� animacje, albo co� innego~ p�ki co przeszkoda po prostu umiera x3
            train.GetComponent<TrainController>().Derail();
        }
    }
    void ColdSourceAction(GameObject train)
    {
        train.GetComponent<TrainStatus>().temperature--;
        if (train.GetComponent<TrainStatus>().temperature < -1)
        {
            //mo�na najpierw da� jak�� animacje, albo co� innego~ p�ki co przeszkoda po prostu umiera x3
            train.GetComponent<TrainController>().Derail();
        }
    }
    void HeatObstacleAction(GameObject train)
    {
        //Gor�ca przeszkoda jest pokonywana zimnem!!!
        if(train.GetComponent<TrainStatus>().temperature < 0)
        {
            //mo�na najpierw da� jak�� animacje, albo co� innego~ p�ki co przeszkoda po prostu umiera x3
            Destroy(gameObject);
        }
    }
    void ColdObstacleAction(GameObject train)
    {
        //Zimna przeszkoda jest pokonywana Gor�cem!!!
        if (train.GetComponent<TrainStatus>().temperature > 0)
        {
            //mo�na najpierw da� jak�� animacje, albo co� innego~ p�ki co przeszkoda po prostu umiera x3
            Destroy(gameObject);
        }
    }
    void NegativePowerSourcleAction(GameObject train)
    {
        train.GetComponent<TrainStatus>().electricCharge++;
    }
    void PositivePowerAction(GameObject train)
    {
        train.GetComponent<TrainStatus>().electricCharge--;
    }
    void PowerReceiverAction(GameObject train)
    {
        //tutaj mo�na zbudowa� jak�� logik� zwi�zan� z przekazywaniem energii do baterii np, gdyby�my mieli kod odpowiedzialny za bateri� x3
        //ewentualnie mo�na tutaj tylko da� przychwycanie Temperatury/Energii dla naszego statycznego obiektu x3
        //do przemy�lenia jeszcze :3
    }






    private void Start()
    {
        CheckWhatAmI();
    }

    public void CheckWhatAmI()
    {
        switch (WhatIsIt)
        {
            case SourceType.HeatSource:
                objectAction = HeatSourceAction;
                break;
            case SourceType.ColdSource:
                objectAction = ColdSourceAction;
                break;
            case SourceType.HeatObstacle:
                objectAction = HeatObstacleAction;
                break;
            case SourceType.ColdObstacle:
                objectAction = ColdObstacleAction;
                break;
            case SourceType.NegativePowerSource:
                objectAction = NegativePowerSourcleAction;
                break;
            case SourceType.PositivePowerSource:
                objectAction = PositivePowerAction;
                break;
            case SourceType.PowerReceiver:
                objectAction = PowerReceiverAction;
                break;
            default:
                objectAction = HeatSourceAction; // Domy�lnie jest �r�d�em Ciep�a.
                break;
        }

        if (isAStation)
        {
            station.OnTrainEntered += DetectStation;
            enabled = false;
        }
        else
        {
            enabled = true;
        }

    }


}
