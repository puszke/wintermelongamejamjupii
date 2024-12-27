using UnityEngine;

public class Zoom : MonoBehaviour
{
    Vector3 defaultPosition;       // Domyœlna pozycja obiektu Balls
    public Transform balls;        // Ballsy!!! potrzebne bardzo do ustawienia!!!
    public Pointer pointer;        // Skrypt Pointer, potrzebne bardzo do ustawienia w editorze!!! 

    public float ZoomedIn = 7f;    // Docelowy zoom przy przybli¿eniu :>
    public float ZoomedOut = 36f;  // Zoom w oddaleniu :O - tak zwany de-zoomifikacja >:3
    public float zoomSpeed = 2f;   // Czas w sekundach na pe³ne przybli¿enie!!! czyli szybkoœæ >:3

    private Vector3 targetPosition; // Docelowa pozycja Balls!!! potrzebna do animacji >.<
    private float targetZoom;       // Docelowa wartoœæ zoomu, te¿ potrzebna do animacji!!! >///<
    private bool isZooming = false; // Bulion, aby wiedzieæ czy nadal animacja zooma siê robi czy nie >:3 (nie ma tutaj buliona od stanu przybli¿enia, przeproszka >.<!!!)
 
    private void Start()
    {
        // Ustawienie domyœlnych wartoœci, takie tam >:3
        defaultPosition = balls.position; 
        targetPosition = defaultPosition; 
        targetZoom = ZoomedOut;           
        Camera.main.orthographicSize = ZoomedOut; 
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && pointer.isOnTerrain)
        {
            // Ustawiamy docelowo gdzie ma siê przybli¿yæ gdy klikniemy myszk¹ prawym przyciskiem >:3
            targetPosition = pointer.mousePositionOnMap;
            targetZoom = ZoomedIn;
            isZooming = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            // I to samo, ale na odwrót, docelowy powrót do domyœlnych wartoœci - takie ustawianko w³aœnie :3
            targetPosition = defaultPosition;
            targetZoom = ZoomedOut;
            isZooming = true;
        }

        //ballsy zawsze s¹ tam gdzie targetowy positione >:3 (czy domyœlnie na œrodku mapy, albo tam gdzie myszka nacisne³a siê jak j¹ wciœniemy :3)
        balls.position = targetPosition;

        // P³ynne przejœcie zoomu kamery!!! tak o!!! ca³e kradzione!!! Lerp larpowany jest tak o!! >:3
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);

        // Zakañczanie animacji gdy obiekt jest blisko do animacji, coœ tam coœ tam, tak musi po prostu byæ, mnie nie pytaj!!! x3
        if (Vector3.Distance(balls.position, targetPosition) < 0.01f &&
            Mathf.Abs(Camera.main.orthographicSize - targetZoom) < 0.01f)
        {
            isZooming = false; // no i zooming falsowany jest tak o :3
        }
    }
}
