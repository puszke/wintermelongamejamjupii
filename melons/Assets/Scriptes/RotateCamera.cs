using UnityEngine;
using UnityEngine.UIElements;

public class RotateCamera : MonoBehaviour
{

    public float rotationSpeed = 160f; // Jak szybko ma siê obracaæ!!! :O
    public float rotationAngle = 90f; // O ile ma siê obracaæ >:3
    private bool isRotating = false;  // Czy siê obraca :3
    private Transform ChildTrans;

    private void Start()
    {
        ChildTrans = transform.Find("CameraPiwoPoint");
    }
    void Update()
    {
        // Sprawdzanie wejœcia klawiszy
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            StartCoroutine(RotateToAngle(ChildTrans, -rotationAngle)); //obracam w prawo na przycisk e :3
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            StartCoroutine(RotateToAngle(ChildTrans, rotationAngle)); //obracam w lewo na przycisk q :3
        }
    }

    private System.Collections.IEnumerator RotateToAngle(Transform target, float angle)
    {
        isRotating = true;

        // Pocz¹tkowy k¹t kamery (zachowujemy k¹t X, Y zmieniamy >:3)
        Vector3 startRotation = target.transform.eulerAngles;
        float startAngleY = startRotation.y;
        float targetAngleY = startAngleY + angle;

        // Obs³uga interpolacji
        float elapsedTime = 0f;
        float duration = Mathf.Abs(angle) / rotationSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolacja k¹ta w osi Y
            float currentAngleY = Mathf.Lerp(startAngleY, targetAngleY, elapsedTime / duration);

            // Ustawianie nowego k¹ta rotacji, zachowuj¹c k¹t X i Z!!! Yupie!!!
            target.transform.rotation = Quaternion.Euler(startRotation.x, currentAngleY, startRotation.z);

            yield return null;
        }

        // Ustawienie dok³adnego k¹ta na koniec :3
        target.transform.rotation = Quaternion.Euler(startRotation.x, targetAngleY, startRotation.z);
        isRotating = false;
    }
}
