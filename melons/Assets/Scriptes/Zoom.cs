using UnityEngine;

public class Zoom : MonoBehaviour
{
    Vector3 defaultPosition;       // Domy�lna pozycja obiektu Balls
    public Transform balls;        // Ballsy!!! potrzebne bardzo do ustawienia!!!
    public Pointer pointer;        // Skrypt Pointer, potrzebne bardzo do ustawienia w editorze!!! 

    public float ZoomedIn = 7f;    // Docelowy zoom przy przybli�eniu :>
    public float ZoomedOut = 36f;  // Zoom w oddaleniu :O - tak zwany de-zoomifikacja >:3
    public float zoomSpeed = 2f;   // Czas w sekundach na pe�ne przybli�enie!!! czyli szybko�� >:3

    private Vector3 targetPosition; // Docelowa pozycja Balls!!! potrzebna do animacji >.<
    private float targetZoom;       // Docelowa warto�� zoomu, te� potrzebna do animacji!!! >///<
    private bool isZooming = false; // Bulion, aby wiedzie� czy nadal animacja zooma si� robi czy nie >:3 (nie ma tutaj buliona od stanu przybli�enia, przeproszka >.<!!!)
 
    private void Start()
    {
        // Ustawienie domy�lnych warto�ci, takie tam >:3
        defaultPosition = balls.position; 
        targetPosition = defaultPosition; 
        targetZoom = ZoomedOut;           
        Camera.main.orthographicSize = ZoomedOut; 
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && pointer.isOnTerrain)
        {
            // Ustawiamy docelowo gdzie ma si� przybli�y� gdy klikniemy myszk� prawym przyciskiem >:3
            targetPosition = pointer.mousePositionOnMap;
            targetZoom = ZoomedIn;
            isZooming = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            // I to samo, ale na odwr�t, docelowy powr�t do domy�lnych warto�ci - takie ustawianko w�a�nie :3
            targetPosition = defaultPosition;
            targetZoom = ZoomedOut;
            isZooming = true;
        }

        //ballsy zawsze s� tam gdzie targetowy positione >:3 (czy domy�lnie na �rodku mapy, albo tam gdzie myszka nacisne�a si� jak j� wci�niemy :3)
        balls.position = targetPosition;

        // P�ynne przej�cie zoomu kamery!!! tak o!!! ca�e kradzione!!! Lerp larpowany jest tak o!! >:3
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);

        // Zaka�czanie animacji gdy obiekt jest blisko do animacji, co� tam co� tam, tak musi po prostu by�, mnie nie pytaj!!! x3
        if (Vector3.Distance(balls.position, targetPosition) < 0.01f &&
            Mathf.Abs(Camera.main.orthographicSize - targetZoom) < 0.01f)
        {
            isZooming = false; // no i zooming falsowany jest tak o :3
        }
    }
}
