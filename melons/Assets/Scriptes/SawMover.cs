using UnityEngine;

public class SawMover : MonoBehaviour
{
    [SerializeField] private Vector3 offset; // Odleg³oœæ do drugiego punktu
    [SerializeField] private float speed = 2f; // Prêdkoœæ poruszania siê

    private Vector3 startPoint; // Punkt pocz¹tkowy
    private Vector3 endPoint; // Punkt koñcowy
    private bool movingToEnd = true; // Czy porusza siê w stronê koñca?

    private void Start()
    {
        startPoint = transform.position;
        endPoint = startPoint + offset;
    }

    private void Update()
    {
        MoveSaw();
    }

    private void MoveSaw()
    {
        // Okreœl cel (endPoint lub startPoint)
        Vector3 target = movingToEnd ? endPoint : startPoint;

        // Poruszaj siê w stronê celu
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Jeœli osi¹gnêliœmy cel, zmieñ kierunek
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            movingToEnd = !movingToEnd;
        }
    }

    
}
