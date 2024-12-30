using UnityEngine;

public class SawMover : MonoBehaviour
{
    [SerializeField] private Vector3 offset; // Odleg�o�� do drugiego punktu
    [SerializeField] private float speed = 2f; // Pr�dko�� poruszania si�

    private Vector3 startPoint; // Punkt pocz�tkowy
    private Vector3 endPoint; // Punkt ko�cowy
    private bool movingToEnd = true; // Czy porusza si� w stron� ko�ca?

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
        // Okre�l cel (endPoint lub startPoint)
        Vector3 target = movingToEnd ? endPoint : startPoint;

        // Poruszaj si� w stron� celu
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Je�li osi�gn�li�my cel, zmie� kierunek
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            movingToEnd = !movingToEnd;
        }
    }

    
}
