using System;
using System.Collections;
using UnityEngine;

public class Station : MonoBehaviour
{
    public int trailsAvailable = 100; // Liczba torów dostêpnych do dodania przez stacjê
    public float detectionRadius = 3f; // Promieñ wykrywania poci¹gów
    public bool infinity = false;
    public int maxUses = 1; // Maksymalna liczba u¿yæ stacji

    public GameObject icon; // Ikona stacji

    public GameObject trainAtStation; // Aktualnie przechwycony poci¹g
    private int currentUses = 0; // Liczba dotychczasowych u¿yæ
    public bool isUsed = false; // Czy stacja jest obecnie u¿ywana
    private bool visited = false; // Czy stacja zosta³a odwiedzona
    public float delay = 1f;

    private bool shouldShowing = false;


    public event Action OnTrainEntered;

    private void Start()
    {
        icon.SetActive(false); // Ukryj ikonê na pocz¹tku
        GetComponent<BoxCollider>().enabled = false;

    }

    private void Update()
    {
        // Jeœli stacja nie jest u¿ywana i ma dostêpne u¿ycia
        if (!isUsed)
        {
            if (currentUses < maxUses || infinity)
            {
                DetectTrain();
            }
            
        }

        // Ustaw ikonê w zale¿noœci od stanu stacji
        icon.SetActive(shouldShowing);
    }

    private void DetectTrain()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < detectionRadius)
            {
                if (!player.GetComponent<TrainController>().derailed)
                {
                    CaptureTrain(player);
                    VisitStation();
                }

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
                        trainController.isRiding = false; // Wstrzymaj poci¹g
                    }

                    currentUses++;
                    shouldShowing= GetComponent<BoxCollider>().enabled = true;
                    isUsed = true; // Oznacz stacjê jako u¿ywan¹
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
            visited = true; // Oznacz stacjê jako odwiedzon¹
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
                
                trainController.isRiding = true; // Wznów ruch poci¹gu
            }

            trainAtStation = null; // Usuñ referencjê do poci¹gu
            shouldShowing = GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(BallsThree(delay)); // Oznacz stacjê jako woln¹

            
        }
    }

    IEnumerator BallsThree(float time)
    {
        yield return new WaitForSeconds(time);
        isUsed = false;
    }


}
