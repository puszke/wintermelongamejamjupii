using UnityEngine;

public class Zoom : MonoBehaviour
{
    Vector3 defaultPosition;
    public Transform balls;
    public Pointer pointer;

    public int ZoomedIn = 7;
    public int ZoomedOut = 36;

    private void Start()
    {
        defaultPosition = balls.position;

    }
    void Update()
    {
        if (Input.GetMouseButton(1) && pointer.isOnTerrain)
        {
            balls.position = pointer.mousePositionOnMap;
            Camera.main.orthographicSize = ZoomedIn;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            balls.position = defaultPosition;
            Camera.main.orthographicSize = ZoomedOut;
        }

    }

}
