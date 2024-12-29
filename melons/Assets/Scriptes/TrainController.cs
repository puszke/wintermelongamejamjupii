using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{

    public string COLOR = "RED";

    public float speed = 5f; // Prêdkoœæ poruszania siê lokomotywy
    public Transform frontDetector; // Punkt wykrywaj¹cy kolejne tory
    public float detectionRadius = 0.5f; // Promieñ wykrywania torów
    public LayerMask trackLayer; // Warstwa, na której znajduj¹ siê tory

    private Transform currentTrack; // Obecny tor, na którym znajduje siê lokomotywa
                                    //private Queue<Transform> visitedTracks = new Queue<Transform>(); // Kolejka odwiedzonych toró

    public bool rightOrder = true;

    public GameObject explosion;

    public bool derailed = false;

    public bool isRiding = false;

    void Update()
    {
        if (!derailed && isRiding)
        {
            if (currentTrack == null)
            {
                DetectNextTrack();
            }

            if (currentTrack != null)
            {
                MoveAlongTrack();
            }
            else
            {
                Derail();
            }
        }
    }

    void DetectNextTrack()
    {
        // Wykrywanie kolejnych torów w zasiêgu frontDetector
        Collider[] detected = Physics.OverlapSphere(frontDetector.position, detectionRadius, trackLayer);

        foreach (var collider in detected)
        {
            Transform track = collider.transform;
            var trackScript = track.GetComponent<BallAp>();

            if (trackScript != null && !trackScript.wasVisited) // Sprawdzamy, czy tor nie by³ odwiedzony
            {
                currentTrack = track;
                trackScript.VisitingTrack(); // Wywo³anie metody VisitingTrack na torze
                //visitedTracks.Enqueue(track);
                break;
            }
        }
    }

    void MoveAlongTrack()
    {
        // Obracanie i przesuwanie w stronê kolejnego toru
        Vector3 direction = currentTrack.position - transform.position;
        direction.y = 0; // Ignorujemy ró¿nicê wysokoœci, chyba ¿e tory s¹ nachylone

        if (direction.magnitude > 0.1f)
        {
            // Przesuwanie w stronê pozycji toru
            Vector3 targetPosition = new Vector3(currentTrack.position.x, transform.position.y, currentTrack.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (rightOrder)
            {
                transform.rotation = currentTrack.rotation;
            }
            else
            {

                transform.rotation = currentTrack.rotation * Quaternion.AngleAxis(180, Vector3.up);
            }
            // Obracanie lokomotywy w kierunku ruchu
            /*if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            }*/
        }
        else
        {
            currentTrack = null; // Lokomotywa dotar³a do aktualnego toru
        }
    }
    void Derail()
    {
        Debug.Log("Train derailed! No tracks ahead.");
        derailed = true;
        GameObject skibidiwybuch = Instantiate(explosion, transform.position, Quaternion.identity);
        // Mo¿esz dodaæ tutaj logikê obs³ugi wykolejenia, np. zatrzymanie poci¹gu czy wyœwietlenie komunikatu

    }

    private void OnDrawGizmos()
    {
        if (frontDetector != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(frontDetector.position, detectionRadius);
        }
    }
}

