using UnityEngine;

public class Pointer : MonoBehaviour
{
    public LayerMask terrainLayer; // layer kt�ry jest fajny do sprawdzania myszy :3
    public Vector3 mousePositionOnMap; //POZYCJA MYSKI >:3

    public bool isOnTerrain = false;

    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; //typowy raycast kt�ry mi kto� powiedzia� �e taki jest fajny :3 (nie wiedzia�em o istnieniu Screen Point to ray)


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayer))
        {
            mousePositionOnMap = hit.point; //aktualizuje informacje gdzie jest myszka, w zale�no�ci gdzie trafi� raycast :<
            isOnTerrain = true;
        }
        else
        {
            isOnTerrain = false;
        }
    }
}