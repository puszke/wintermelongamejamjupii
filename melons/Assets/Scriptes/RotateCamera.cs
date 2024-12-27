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

        // Pocz¹tkowy i koñcowy k¹t kamery :O
        float startAngle = target.transform.eulerAngles.y;
        float targetAngle = startAngle + angle;

        // Obs³uga interpolacji
        float elapsedTime = 0f;
        float duration = Mathf.Abs(angle) / rotationSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolacja k¹ta
            float currentAngle = Mathf.Lerp(startAngle, targetAngle, elapsedTime / duration);
            target.transform.rotation = Quaternion.Euler(0, currentAngle, 0);

            yield return null;
        }

        // Ustawienie dok³adnego k¹ta na koniec
        target.transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        isRotating = false;
    }
}
