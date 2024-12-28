using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    // Lista pocz¹tkowych punktów torów
    public List<GameObject> startPoints = new List<GameObject>();
    // Lista koñcowych punktów torów
    public List<GameObject> endPoints = new List<GameObject>();

    // Dodaj nowy tor (pocz¹tek i koniec)
    public void AddPath(GameObject startPoint, GameObject endPoint)
    {
        if (!startPoints.Contains(startPoint))
        {
            startPoints.Add(startPoint);
        }

        if (!endPoints.Contains(endPoint))
        {
            endPoints.Add(endPoint);
        }
    }

    // Usuwanie toru i aktualizacja
    public void RemovePath(GameObject point)
    {
        // Sprawdzenie, czy punkt jest wœród punktów pocz¹tkowych
        if (startPoints.Contains(point))
        {
            int index = startPoints.IndexOf(point);
            if (index >= 0 && index < endPoints.Count)
            {
                // Usuwamy punkt pocz¹tkowy i koñcowy
                GameObject endPoint = endPoints[index];
                startPoints.RemoveAt(index);
                endPoints.RemoveAt(index);

                // Je¿eli usuniêty punkt mia³ przypisany punkt koñcowy, sprawdzamy czy nie jest on pocz¹tkiem kolejnego toru
                if (endPoint != null && startPoints.Contains(endPoint))
                {
                    // Usuwamy ten punkt z listy startPoints
                    RemovePath(endPoint);
                }
            }
        }
    }

    // Sprawdzenie, czy dany punkt jest pocz¹tkiem toru
    public bool IsStartOfPath(GameObject point)
    {
        return startPoints.Contains(point);
    }
}
