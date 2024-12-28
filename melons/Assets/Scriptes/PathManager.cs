using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    // Lista pocz�tkowych punkt�w tor�w
    public List<GameObject> startPoints = new List<GameObject>();
    // Lista ko�cowych punkt�w tor�w
    public List<GameObject> endPoints = new List<GameObject>();

    // Dodaj nowy tor (pocz�tek i koniec)
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
        // Sprawdzenie, czy punkt jest w�r�d punkt�w pocz�tkowych
        if (startPoints.Contains(point))
        {
            int index = startPoints.IndexOf(point);
            if (index >= 0 && index < endPoints.Count)
            {
                // Usuwamy punkt pocz�tkowy i ko�cowy
                GameObject endPoint = endPoints[index];
                startPoints.RemoveAt(index);
                endPoints.RemoveAt(index);

                // Je�eli usuni�ty punkt mia� przypisany punkt ko�cowy, sprawdzamy czy nie jest on pocz�tkiem kolejnego toru
                if (endPoint != null && startPoints.Contains(endPoint))
                {
                    // Usuwamy ten punkt z listy startPoints
                    RemovePath(endPoint);
                }
            }
        }
    }

    // Sprawdzenie, czy dany punkt jest pocz�tkiem toru
    public bool IsStartOfPath(GameObject point)
    {
        return startPoints.Contains(point);
    }
}
