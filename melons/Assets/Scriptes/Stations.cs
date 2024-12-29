using System;
using System.Collections;
using UnityEngine;

public class Station : MonoBehaviour
{
    public int trailsAvailable = 100; // Liczba tor�w dost�pnych do dodania przez stacj�
    public float detectionRadius = 2f; // Promie� wykrywania poci�g�w
    public bool infinity = false;
    public int maxUses = 1; // Maksymalna liczba u�y� stacji

    public GameObject icon; // Ikona stacji

    public GameObject trainAtStation; // Aktualnie przechwycony poci�g
    private int currentUses = 0; // Liczba dotychczasowych u�y�
    public bool isUsed = false; // Czy stacja jest obecnie u�ywana
    private bool visited = false; // Czy stacja zosta�a odwiedzona
    public float delay = 1f;

    private bool shouldShowing = false;


    public event Action OnTrainEntered;

    private void Start()
    {
        icon.SetActive(false); // Ukryj ikon� na pocz�tku
    }

    private void Update()
    {
        // Je�li stacja nie jest u�ywana i ma dost�pne u�ycia
        if (!isUsed)
        {
            if (currentUses < maxUses || infinity)
            {
                DetectTrain();
            }
            
        }

        // Ustaw ikon� w zale�no�ci od stanu stacji
        icon.SetActive(shouldShowing);
    }

    private void DetectTrain()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < detectionRadius)
            {
                CaptureTrain(player);
                VisitStation();

                break;
            }
        }
    }

    private void CaptureTrain(GameObject train)
    {
        if (trainAtStation == null)
        {
            
            if (infinity || (currentUses < maxUses))
            {
                

                if (isUsed == false)
                {

                    trainAtStation = train;
                    TrainController trainController = train.GetComponent<TrainController>();

                    if (trainController != null)
                    {
                        trainController.isRiding = false; // Wstrzymaj poci�g
                    }

                    currentUses++;
                    shouldShowing= true;
                    isUsed = true; // Oznacz stacj� jako u�ywan�
                    OnTrainEntered?.Invoke();
                }


            }
        }
    }

    private void VisitStation()
    {
        if (!visited)
        {
            GlobalTrailManager.instance.AddTracks(trailsAvailable); // Dodaj tory do globalnego licznika
            visited = true; // Oznacz stacj� jako odwiedzon�
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && trainAtStation != null)
        {
            ReleaseTrain();
        }
    }

    private void ReleaseTrain()
    {
        if (trainAtStation != null)
        {
            
            isUsed = true;
            TrainController trainController = trainAtStation.GetComponent<TrainController>();

            if (trainController != null)
            {
                
                trainController.isRiding = true; // Wzn�w ruch poci�gu
            }

            trainAtStation = null; // Usu� referencj� do poci�gu
            shouldShowing= false;
            StartCoroutine(BallsThree(delay)); // Oznacz stacj� jako woln�

            
        }
    }

    IEnumerator BallsThree(float time)
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(time);
        GetComponent<BoxCollider>().enabled = true;
        isUsed = false;
    }


}
