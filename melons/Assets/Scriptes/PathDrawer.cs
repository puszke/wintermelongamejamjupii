using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public GameObject pointPrefab; // Prefab punktu do ustawienia na �cie�ce
    public float minDistance = 4f; // Minimalna odleg�o�� mi�dzy punktami
    public LayerMask pathLayer;   // Warstwa, kt�ra przechowuje ju� istniej�ce tory/punkty

    private List<GameObject> points = new List<GameObject>();
    private bool isDrawing = false;
    private Vector3 lastPointPosition;

    public Pointer pointer;
    public PathManager pathManager;

    private int trailNum = 0;

    private string currentTag = "";

    void Update()
    {

        if (Input.GetMouseButtonDown(0)&&!Input.GetKey(KeyCode.LeftShift)&&GlobalTrailManager.instance.trailNum>0 )
        {
            StartDrawing();
            
        }
        else if (Input.GetMouseButton(0) && isDrawing)
        {
            ContinueDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }
    }

    void StartDrawing()
    {
        //Debug.Log("Zaczynam rysowa�!!!");
        isDrawing = true;
        Vector3 startPosition = GetMouseWorldPosition();

        if (pointer.isOnTerrain && !pointer.isOnPoint && currentTag == "Buildable")
        {
            GlobalTrailManager.instance.trailNum--;
            CreatePoint(startPosition);
            lastPointPosition = startPosition;
        }
        else
        {
            StopDrawing();
        }
    }

    void ContinueDrawing()
    {
        //Debug.Log("Dalej rysuje!!!");
        Vector3 currentPosition = GetMouseWorldPosition();
        //Debug.Log(currentPosition);

        if (Vector3.Distance(lastPointPosition, currentPosition) >= minDistance && pointer.isOnTerrain && !pointer.isOnPoint )
        {
            if (GlobalTrailManager.instance.trailNum > 0 && currentTag == "Buildable")
            {
                GlobalTrailManager.instance.trailNum--;
                CreatePoint(currentPosition);
                RotatePreviousPoint(currentPosition);
                lastPointPosition = currentPosition;
            }
        }

    }

    public void StopDrawing()
    {
        //Debug.Log("Ko�cz� rysowa�!!!");
        isDrawing = false;
        points.Clear(); // Opcjonalnie czy�cimy list�, je�li nie musimy trzyma� punkt�w.
    }

    void CreatePoint(Vector3 position)
    {
        GameObject newPoint = Instantiate(pointPrefab, position, Quaternion.identity);
        newPoint.transform.name = trailNum.ToString();
        trailNum++;
        points.Add(newPoint);
    }

    void RotatePreviousPoint(Vector3 targetPosition)
    {
        if (points.Count > 1)
        {
            GameObject previousPoint = points[points.Count - 2];
            previousPoint.transform.LookAt(targetPosition);
            //Debug.Log("obr�ci�em poprzedniego ziomka!!!");
        }
    }

    /*bool IsPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, minDistance, pathLayer);
        return colliders.Length == 0; // Je�li w pobli�u nie ma kolizji, pozycja jest poprawna.
    }*/

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, pathLayer))
        {
            currentTag = hitInfo.transform.tag;
            Debug.Log(currentTag);
            return hitInfo.point;
        }
        return Vector3.zero;
    }


}

